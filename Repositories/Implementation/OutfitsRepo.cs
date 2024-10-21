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
    public class OutfitsRepo : IOutfitsInterface
    {
        private readonly StoreContext _context;
       public OutfitsRepo(StoreContext context)
       {
            _context = context;
       }
        public async Task<Outfits> CreateOutfit(Outfits outfits)
        {
            await _context.Outfits.AddAsync(outfits);
           return outfits;
        }

        public bool Delete(Outfits outfits)
        {
            _context.Outfits.Remove(outfits);
            return true;
        }

        public async Task<ICollection<Outfits>> GetAllOutfit()
        {
            var outs = await _context.Outfits.ToListAsync();
            return outs;
        }

        public async Task<Outfits> GetById(Guid id)
        {
            var outfit = await _context.Outfits.FirstOrDefaultAsync(r => r.Id == id);
            return outfit;
        }

        public Outfits Update(Outfits outfits)
        {
            _context.Outfits.Update(outfits);
            return outfits;
        }
    }
}