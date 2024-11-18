using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class RoleResponseModel
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }

    public class RoleRequestModel
    {
        public string Name { get; set; } = default!;
    }
}