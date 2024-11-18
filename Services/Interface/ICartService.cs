using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface ICartService
    {
        Task<Response<AddToCartResponseModel>> AddToCart (CreateOrderRequest model);
        Task<Response<AddToCartResponseModel>> GetCartByUserId (Guid userId);
        Task<Response<AddToCartResponseModel>> DeleteCartByUserId (Guid userId);
        Task<Response<AddToCartResponseModel>> Update (CreateOrderRequest model, Guid id);
        Task<Response<ICollection<AddToCartResponseModel>>> GetAllCarts ();
    }
}