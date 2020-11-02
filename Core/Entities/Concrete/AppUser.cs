using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    [Table("AppUsers", Schema = "User")]
    public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Type = typeof(AppUser).Name;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled => LockoutEnd != null ? (LockoutEnd >= DateTimeOffset.UtcNow) : false;
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IList<AppRole> AppRoles { get; set; } = new List<AppRole>();
    }
}
