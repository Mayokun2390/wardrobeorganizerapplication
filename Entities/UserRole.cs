using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class UserRole
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } 
        public User User { get; set; } = default!;
        public Guid RoleId { get; set; } 
        public Role Role { get; set; } = default!;
    }
}
