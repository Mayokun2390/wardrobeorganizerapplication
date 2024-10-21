using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class RoleRepo : IRoleInterface
    {
        private readonly StoreContext _context;
        public RoleRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<Role> CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
           return role;
        }

        public bool Delete(Role role)
        {
            _context.Roles.Remove(role);
            return true;
        }

        public async Task<ICollection<Role>> GetAllRoles()
        {
            var role = await _context.Roles.ToListAsync();
            return role;
        }

        public async Task<Role> GetBy(Expression<Func<Role, bool>> predicate)
        {
            var getRole = await _context.Roles.FirstOrDefaultAsync(predicate);
            return getRole;
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return role;
        }

        public Role Update(Role role)
        {
            _context.Roles.Update(role);
            return role;
        }
    }
}