using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IProductService
    {
        Task<Response<ProductResponseModel>> CreateProduct (ProductRequestModel model);
        Task<Response<ProductResponseModel>> Get (Guid id);
        Task<Response<ICollection<ProductResponseModel>>> GetAllProducts ();
        Task<Response<ICollection<ProductResponseModel>>> GetAllOutfits();
        Task<Response<ICollection<ProductResponseModel>>> GetAllClothingItems();
        Task<Response<ICollection<ProductResponseModel>>> GetAllClothings();
        Task<Response<ICollection<ProductResponseModel>>> GetAllFootWears();
        Task<Response<ICollection<ProductResponseModel>>> GetAllAccessories();
        Task<Response<ProductResponseModel>> Update (ProductRequestModel model, Guid id);
        Task<Response<ProductResponseModel>> Delete (Guid id);
    }
}