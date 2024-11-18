using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IUserInterface
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserAsync(Expression<Func<User, bool>> predicate);
        Task<User> GetUserById(Guid id);
        Task<ICollection<User>> GetAllUsers();
        bool Delete(User user);
        User Update(User user);
    }
}