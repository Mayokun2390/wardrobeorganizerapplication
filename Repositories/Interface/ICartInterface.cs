using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface ICartInterface
    {
        Task<Cart> GetCartByUserId(Guid userId);
    Task AddCart(Cart cart);
    Task UpdateCart(Cart cart);
    Task DeleteCartItems(Guid cartId);
        Task<ICollection<Cart>> GetAllCarts();

    }
}