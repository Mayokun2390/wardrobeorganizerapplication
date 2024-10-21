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
    public class CartRepo : ICartInterface
    {
        private readonly StoreContext _context;
        public CartRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<Cart> Create(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return cart;
        }

        public bool Delete(Cart cart)
        {
            _context.Carts.Remove(cart);
            return true;
        }

        public async Task<ICollection<Cart>> GetAllCarts()
        {
            var getCarts = await _context.Carts.ToListAsync();
            return getCarts;
        }

        public async Task<Cart?> GetCart(Guid id)
        {
            var getCart = await _context.Carts .FirstOrDefaultAsync(c => c.Id == id);
            return getCart;
        }

        public Cart Update(Cart cart)
        {
           _context.Carts.Update(cart);
            return cart;
        }
    }
}