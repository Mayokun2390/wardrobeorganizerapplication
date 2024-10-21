using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IOutfitsService
    {
        Task<Response<OutfitsResponseModel>> Create (OutfitsRequestModel model);
        Task<Response<OutfitsResponseModel>> Get (Guid id);
        Task<Response<ICollection<OutfitsResponseModel>>> GetAllOutfits ();
        Task<Response<OutfitsResponseModel>> Update (OutfitsRequestModel model, Guid id);
        Task<Response<OutfitsResponseModel>> Delete (Guid id);
    }
}