using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IOutfitsInterface
    {
        Task<Outfits> CreateOutfit(Outfits outfits);
        Task<Outfits> GetById(Guid id);
        Task<ICollection<Outfits>> GetAllOutfit();
        Outfits Update(Outfits outfits);
        bool Delete (Outfits outfits);
    }
}