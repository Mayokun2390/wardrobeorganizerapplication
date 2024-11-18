using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IOrderService
    {
        Task<Response<OrderResponseModel>> GetOrder (Guid id);
        Task<Response<ICollection<OrderResponseModel>>> GetAllOrders ();
        Task<Response<ICollection<OrderResponseModel>>> GetAllApprovedOrders ();
        Task<Response<ICollection<OrderResponseModel>>> GetAllDisApprovedOrders ();
        Task<Response<OrderResponseModel>> Update (OrderRequestModel model, Guid id);
        Task<Response<OrderResponseModel>> Delete (Guid id);
        Task<Response<OrderResponseModel>> ApproveOrder (Guid id);
        Task<Response<OrderResponseModel>> IsDelivered (Guid id);
        Task<Response<OrderResponseModel>> DisApproved (Guid id);
        Task<Response<OrderResponseModel>> GetApprovedOrder (Guid id);
        Task<Response<OrderResponseModel>> GetDisApprovedOrder (Guid id);

        Task<Response<OrderResponseModel>> CreateOrder (OrderRequestModel model);
        
    }
}
