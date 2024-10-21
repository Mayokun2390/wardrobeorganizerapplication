using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface ICartInterface
    {
        Task<Cart> Create(Cart cart);
       Task<Cart?> GetCart(Guid id);
        Task<ICollection<Cart>> GetAllCarts();
        Cart Update(Cart cart);
       bool  Delete(Cart cart);
    }
}