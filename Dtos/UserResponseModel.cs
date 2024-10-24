using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class UserResponseModel
    {
        public Guid Id{ get; set; } 
        public string Email { get; set; } = default!;
        public string role {get; set; } = default!;
        public string Token { get; set; } = default!;

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }


    public class UserRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }


    public class LoginRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}