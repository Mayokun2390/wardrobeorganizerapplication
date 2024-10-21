using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface ICartService
    {
        Task<Response<CartResponseModel>> AddToCart (CartRequestModel model);
        Task<Response<ICollection<CartResponseModel>>> GetAll ();
        Task<Response<CartResponseModel>> Update (CartRequestModel model);
        Task<Response<CartResponseModel>> Delete (Guid id);
    }
}