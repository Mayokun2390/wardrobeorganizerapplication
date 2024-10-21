using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface ICustomerInterface
    {
        Task<Customer> Create(Customer customer);
        Task<Customer> GetCustomerById(Guid id);
        Task<ICollection<Customer>> GetAllCustomers();
        Customer Update(Customer customer);
        Task<Customer> GetCustomerByEmail(Expression<Func<Customer, bool>> predicate);
        bool Delete(Customer customer);        
    }
}