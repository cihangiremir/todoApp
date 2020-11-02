using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation.AppUser;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Mapper;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dto.AppUser;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private IAppUserDal _appUserDal;
        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }

        [ValidationAspect(typeof(AppUserRegisterInputValidator))]
        public async Task<IDataResult<AppUserOutput>> Add(AppUserRegisterInput registerInput)
        {
            var control = await BusinessRules.RunAsync(CheckUserEmail(registerInput.Email));
            if (!control.Success) return new ErrorDataResult<AppUserOutput>(control.Message);

            var user = MapsterTool.Map<AppUserRegisterInput, AppUser>(registerInput);
            CreatePasswordHash(user, registerInput.Password);
            await _appUserDal.Add(user);

            var userOutput = MapsterTool.Map<AppUser, AppUserOutput>(user);
            return new SuccessDataResult<AppUserOutput>(userOutput, Messages.Successfully);

        }

        public async Task<IResult> Delete(AppUserDeleteInput deleteInput)
        {
            var user = await _appUserDal.Get(t => t.Id == deleteInput.Id);
            if (user == null) return new ErrorResult(Messages.UnSuccessfully);
            await _appUserDal.Delete(user);


            return new SuccessResult(Messages.Successfully);
        }

        public async Task<IDataResult<AppUserOutput>> GetByFilter(Expression<Func<AppUser, bool>> filter)
        {
            var user = await _appUserDal.Get(filter);

            var userOutput = MapsterTool.Map<AppUser, AppUserOutput>(user);

            return new SuccessDataResult<AppUserOutput>(userOutput, Messages.Successfully);
        }

        public async Task<IDataResult<IList<AppUserOutput>>> GetListByFilter(Expression<Func<AppUser, bool>> filter = null)
        {
            var users = await _appUserDal.GetList(filter);
            var userOutputs = MapsterTool.Map<IList<AppUser>, IList<AppUserOutput>>(users);

            return new SuccessDataResult<IList<AppUserOutput>>(userOutputs, Messages.Successfully);
        }

        public async Task<IResult> Update(AppUserUpdateInput updateInput)
        {
            var user = await _appUserDal.Get(t => t.Id == updateInput.Id);
            user.Email = updateInput.Email;
            user.Name = updateInput.Name;
            user.PhoneNumber = updateInput.PhoneNumber;
            user.Surname = updateInput.Surname;

            CreatePasswordHash(user, updateInput.Password);

            await Update(user);

            return new SuccessResult(Messages.Successfully);
        }

        public async Task<IResult> CheckUserEmail(string email)
        {
            var result = await _appUserDal.Get(t => t.Email == email);
            if (result == null) return new SuccessResult();
            return new ErrorResult("Kayıtlı e-posta adresi.");
        }

        public async Task<IDataResult<AppUser>> Login(AppUserLoginInput loginInput)
        {
            var user = await GetAppUserByFilter(t => t.Email == loginInput.Email);
            if (!user.Success) return new ErrorDataResult<AppUser>(Messages.EmailOrPasswordWrong);
            if (user.Data.LockoutEnabled) return new ErrorDataResult<AppUser>(Messages.UserLocked);

            var passwordCheck = HashingHelper.VerifyPasswordHash(loginInput.Password, user.Data.PasswordHash, user.Data.PasswordSalt);
            if (!passwordCheck)
            {
                user.Data.AccessFailedCount++;
                if (user.Data.AccessFailedCount > 5)
                {
                    user.Data.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(10);
                    user.Data.AccessFailedCount = 0;

                    await Update(user.Data);
                    return new ErrorDataResult<AppUser>($"{Messages.TooManyIncorrectLogin}{(user.Data.LockoutEnd - DateTimeOffset.UtcNow).Value.Minutes}");
                }
                await Update(user.Data);
                return new ErrorDataResult<AppUser>(Messages.EmailOrPasswordWrong);
            }
            return new SuccessDataResult<AppUser>(user.Data, Messages.Successfully);
        }

        private async Task<IResult> Update(AppUser appUser)
        {
            await _appUserDal.Update(appUser);
            return new SuccessResult(Messages.Successfully);
        }

        private async Task<IDataResult<AppUser>> GetAppUserByFilter(Expression<Func<AppUser, bool>> filter)
        {
            var user = await _appUserDal.Get(filter);
            if (user == null) new ErrorDataResult<AppUser>(Messages.UnSuccessfully);
            return new SuccessDataResult<AppUser>(user, Messages.Successfully);
        }

        private void CreatePasswordHash(AppUser user, string password)
        {
            byte[] passwordHash,passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }
    }
}
