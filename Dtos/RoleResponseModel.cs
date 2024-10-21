using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class RoleResponseModel
    {
        public Guid Id{ get; set; } 
        public string Name { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class RoleRequestModel
    {
        public string Name { get; set; } = default!;
    }
}