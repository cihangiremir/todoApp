using Entities.Dto.AppRole;
using System.Collections.Generic;

namespace Entities.Dto.AppUser
{
    public class AppUserRegisterInput
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public IList<AppRoleAddInput> AppRoles { get; set; } = new List<AppRoleAddInput>();
    }
}
