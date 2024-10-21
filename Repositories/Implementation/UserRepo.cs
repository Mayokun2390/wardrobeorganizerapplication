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
    public class UserRepo : IUserInterface
    {
        private readonly StoreContext _context;
        public UserRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public bool Delete(User user)
        {
            _context.Users.Remove(user);
            return true;
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            var user = await _context.Users.ToListAsync();
            return user;
        }

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(predicate);
            return getUser;
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }
    }
}