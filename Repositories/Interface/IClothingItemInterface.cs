using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IClothingItemInterface
    {
        Task<ClothingItems> Create(ClothingItems clothingItems);
        Task<ClothingItems> GetById(Guid id);
        Task<ClothingItems> GetByName(string name);
        Task<ICollection<ClothingItems>> GetAllClothingItems();
        ClothingItems Update(ClothingItems clothingItems);
        bool Delete(ClothingItems clothingItems);
    }
}