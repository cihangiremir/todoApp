using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AppUser user, List<AppRole> userRoles);
        AccessToken CreateToken(AppUser user);
    }
}
