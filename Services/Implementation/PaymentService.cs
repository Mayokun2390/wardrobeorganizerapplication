using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayStack.Net;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentInterface _paymentInterface;
        private readonly IUnitOfWork _unitofwork;
        private readonly IConfiguration _configuration;
        private readonly IProductInterface _productInterface;
        private readonly ICustomerInterface _customerInterface;
        private readonly ICurrentUser _currentUser;
        private readonly IUserInterface _userInterface;

        private PayStackApi PayStack { get; set; }
        private readonly string Token;
        public PaymentService(IPaymentInterface paymentInterface, IUnitOfWork unitofwork, IConfiguration configuration, IProductInterface productInterface, ICustomerInterface customerInterface, ICurrentUser currentUser, IUserInterface userInterface)
        {
            _paymentInterface = paymentInterface;
            _unitofwork = unitofwork;
            _configuration = configuration;
            _productInterface = productInterface;
            _customerInterface = customerInterface;
            _currentUser = currentUser;
            _userInterface = userInterface;
            PayStack = new PayStackApi(Token);
            Token = _configuration["PayStack:SecretKey"];
        }
        public async Task<Response<PaymentResponseModel>> Delete(Guid id)
        {
            var payment = await _paymentInterface.GetById(id);
            if (payment == null)
            {
                return new Response<PaymentResponseModel>
                {
                    Message = "Payment not found",
                    Status = false,
                };
            }
            _paymentInterface.Delete(payment);
            _unitofwork.SaveChanges();
            return new Response<PaymentResponseModel>
            {
                Message = "Payment deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<PaymentResponseModel>> Get(Guid id)
        {
            var pay = await _paymentInterface.GetById(id);
            if (pay == null)
            {
                return new  Response<PaymentResponseModel>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }
            var getPayment = new PaymentResponseModel
            {
                Amount = pay.Amount,
            };
            return new Response<PaymentResponseModel>
            {
                Message = "Payment Exist",
                Status = true,
                Value = getPayment,
            };
        }

        public async Task<Response<ICollection<PaymentResponseModel>>> GetAllPayment()
        {
            var payment = await _paymentInterface.GetAllPayment();
            var getPayments = payment.Select(x => new PaymentResponseModel{  
                Amount = x.Amount,
            }).ToList();

            return new Response<ICollection<PaymentResponseModel>>
            {
                Value = getPayments,
                Message = "List of Payments",
                Status = true,
            };
        }

        public async Task<Response<PaymentResponseModel>> MakePayment(PaymentRequestModel model)
        {
            var current = _userInterface.GetUserAsync(x => x.Email == _currentUser.GetCurrentUser());
            if (current == null)
            {
                return 
            }
        }

        public async Task<Response<PaymentResponseModel>> Update(PaymentRequestModel model, Guid id)
        {
           var pay = await _paymentInterface.GetById(id);
            if (pay == null)
            {
                return new Response<PaymentResponseModel>
                {
                    Message = "Payment not found",
                    Status = false,
                };
            }
            var updatePayment = new Payment();
            pay.Amount = model.Amount;
            _paymentInterface.Update(updatePayment);
            _unitofwork.SaveChanges();
            return new Response<PaymentResponseModel>
            {
                Message = "Payment Updated",
                Status = true,
            };
        }
    }
}