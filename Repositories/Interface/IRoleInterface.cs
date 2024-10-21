using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IRoleInterface
    {
        Task<Role> CreateRole(Role role);
        Task<Role> GetRoleById(Guid id);
        Task<ICollection<Role>> GetAllRoles();
        bool Delete(Role role);
        Role Update (Role role);
        Task<Role> GetBy(Expression<Func<Role, bool>> predicate);
    }
}