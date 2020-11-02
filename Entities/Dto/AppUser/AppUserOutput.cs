using Entities.Dto.AppRole;
using System;
using System.Collections.Generic;

namespace Entities.Dto.AppUser
{
    public class AppUserOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public IList<AppRoleOutput> AppRoles { get; set; } = new List<AppRoleOutput>();
    }
}
