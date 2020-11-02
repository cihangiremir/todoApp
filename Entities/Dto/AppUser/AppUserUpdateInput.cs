using Entities.Dto.AppRole;
using System;
using System.Collections.Generic;

namespace Entities.Dto.AppUser
{
    public class AppUserUpdateInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public IList<AppRoleUpdateInput> AppRoles { get; set; } = new List<AppRoleUpdateInput>();
    }
}
