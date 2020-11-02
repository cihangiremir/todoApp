using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dto.AppUser;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        public Task<IDataResult<AccessToken>> Login(AppUserLoginInput loginInput);
        public Task<IDataResult<AppUserOutput>> Register(AppUserRegisterInput registerInput);
    }
}
