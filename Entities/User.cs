using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class User
    {
        public Guid Id{ get; set; } 
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PasswordSort { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string role {get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}