using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface IPaymentService
    {
        Task<Response<PaymentResponseModel>> MakePayment (Guid id);
        Task<Response<PaymentResponseModel>> Get (Guid id);
        Task<Response<ICollection<PaymentResponseModel>>> GetAllPayment ();
        Task<Response<ICollection<PaymentResponseModel>>> GetAllCompletedPayment ();
        Task<Response<ICollection<PaymentResponseModel>>> GetAllFailedPayment ();
        Task<Response<ICollection<PaymentResponseModel>>> GetAllPendingPayment ();
        Task<Response<PaymentResponseModel>> Update (PaymentRequestModel model, Guid id);
        Task<Response<PaymentResponseModel>> Delete (Guid id);
    }
}