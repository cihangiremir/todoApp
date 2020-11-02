using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dto.AppUser;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppUserService
    {
        public Task<IDataResult<AppUserOutput>> Add(AppUserRegisterInput registerInput);
        public Task<IResult> Update(AppUserUpdateInput updateInput);
        public Task<IResult> Delete(AppUserDeleteInput deleteInput);
        public Task<IDataResult<IList<AppUserOutput>>> GetListByFilter(Expression<Func<AppUser, bool>> filter = null);
        public Task<IDataResult<AppUserOutput>> GetByFilter(Expression<Func<AppUser, bool>> filter);
        public Task<IDataResult<AppUser>> Login(AppUserLoginInput loginInput);
    }
}
