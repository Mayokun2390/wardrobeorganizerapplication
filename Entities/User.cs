using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class User
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}

