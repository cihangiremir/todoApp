using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation.AppUser;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dto.AppUser;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IAppUserService _appUserManager;
        private ITokenHelper _tokenHelper;
        public AuthManager(IAppUserService appUserManager, ITokenHelper tokenHelper)
        {
            _appUserManager = appUserManager;
            _tokenHelper = tokenHelper;
        }

        [ValidationAspect(typeof(AppUserLoginInputValidator))]
        public async Task<IDataResult<AccessToken>> Login([FromBody] AppUserLoginInput loginInput)
        {
            var user = await _appUserManager.Login(loginInput);
            if (!user.Success) return new ErrorDataResult<AccessToken>(user.Message);

            var userToken = _tokenHelper.CreateToken(user.Data);

            return new SuccessDataResult<AccessToken>(userToken, Messages.Successfully);

        }
        [ValidationAspect(typeof(AppUserRegisterInputValidator))]
        public async Task<IDataResult<AppUserOutput>> Register([FromBody] AppUserRegisterInput registerInput)
        {
            var result = await _appUserManager.Add(registerInput);
            if (!result.Success) return new ErrorDataResult<AppUserOutput>(result.Message);
            return new SuccessDataResult<AppUserOutput>(result.Data, Messages.Successfully);
        }
    }
}
