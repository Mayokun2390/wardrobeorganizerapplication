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
    public class ClothingItemsRepo : IClothingItemInterface
    {
        private readonly StoreContext _context;
        public ClothingItemsRepo(StoreContext context)
        {
            _context = context;
        }
        
        public async Task<ClothingItems> Create(ClothingItems clothingItems)
        {
            await _context.ClothingItems.AddAsync(clothingItems);
            return clothingItems;
        }

        public bool Delete(ClothingItems clothingItems)
        {
            _context.ClothingItems.Remove(clothingItems);
            return true;
        }

        public async Task<ICollection<ClothingItems>> GetAllClothingItems()
        {
            var getCustomers = await _context.ClothingItems.ToListAsync();
            return getCustomers;
        }

        public async Task<ClothingItems> GetById(Guid id)
        {
            var getId = await _context.ClothingItems.FirstOrDefaultAsync(c => c.Id == id);
            return getId;
        }

        public async Task<ClothingItems> GetByName(string name)
        {
            var getName = await _context.ClothingItems.FirstOrDefaultAsync(c => c.Name == name);
            return getName;
        }

        public ClothingItems Update(ClothingItems clothingItems)
        {
            _context.ClothingItems.Update(clothingItems);
            return clothingItems;
        }
    }
}