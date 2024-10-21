using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;

namespace WardrobeOrganizerApp.Services.Interface
{
    public interface ICustomerService
    {
        Task<Response<CustomerResponseModel>> CreateCustomer (CustomerRequestModel model);
        Task<Response<CustomerResponseModel>> Get (string email);
        Task<Response<CustomerResponseModel>> GetById (Guid id);
        Task<Response<ICollection<CustomerResponseModel>>> GetAllCustomers ();
        Task<Response<CustomerResponseModel>> Update (CustomerRequestModel model, Guid id);
        Task<Response<CustomerResponseModel>> Delete (Guid id);
    }
}