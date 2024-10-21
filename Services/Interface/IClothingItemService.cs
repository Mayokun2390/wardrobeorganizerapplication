using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IClothingItemService
    {
        Task<Response<ClothingItemResponseModel>> Create (ClothingItemRequestModel model);
        Task<Response<ClothingItemResponseModel>> Get (Guid id);
        Task<Response<ICollection<ClothingItemResponseModel>>> GetAllClothings ();
        Task<Response<ClothingItemResponseModel>> Update (ClothingItemRequestModel model, Guid id);
        Task<Response<ClothingItemResponseModel>> Delete (Guid id);
    }
}