using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class ProductRepo : IProductInterface
    {
        private readonly StoreContext _context;
        public ProductRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
           return product;
        }

        public bool Delete(Product product)
        {
            _context.Products.Remove(product);
            return true;
        }

        public async Task<ICollection<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetById(Guid id)
        {
            var pro = await _context.Products.Include(o => o.cart).FirstOrDefaultAsync(r => r.Id == id);
            return pro;
        }

        public async Task<Product> GetByName(string name)
        {
            var pro = await _context.Products.FirstOrDefaultAsync(r => r.Name == name);
            return pro;
        }

        public Product Update(Product product)
        {
            _context.Products.Update(product);
            return product;
        }
    }
}