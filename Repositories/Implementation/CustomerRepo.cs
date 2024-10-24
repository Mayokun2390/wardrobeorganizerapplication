using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class CustomerRepo : ICustomerInterface
    {
        private readonly StoreContext _context;
        public CustomerRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<Customer> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return customer;
        }

        public bool Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            return true;
        }

        public async Task<ICollection<Customer>> GetAllCustomers()
        {
            var getCustomers = await _context.Customers.ToListAsync();
            return getCustomers;   
        }

        public async Task<Customer> GetCustomerByEmail(Expression<Func<Customer, bool>> predicate)
        {
            var getEmail = await _context.Customers.FirstOrDefaultAsync(predicate);
            return getEmail;
        }

        public async Task<Customer> GetCustomerById(Guid id)
        {
            var getEmail = await _context.Customers.Include(x => x.Order).FirstOrDefaultAsync(c => c.Id == id);
            return getEmail;
        }

        public Customer Update(Customer customer)
        {
            _context.Customers.Update(customer);
            return customer;
        }
    }
}