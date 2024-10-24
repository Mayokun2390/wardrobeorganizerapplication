using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayStack.Net;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;
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

        public async Task<Response<PaymentResponseModel>> MakePayment(Guid id)
        {
            var getcurrentcustomer = await _customerInterface.GetCustomerByEmail(m => m.Email == _currentUser.GetCurrentUser());
            if (getcurrentcustomer == null)
            {
                return new Response<PaymentResponseModel>
                {
                    Message = "Customer not found",
                    Status = false,
                };
            }

            var product = await _productInterface.GetById(id);
            if (product == null)
            {
                return new Response<PaymentResponseModel>
                {
                    Message = "Product not found",
                    Status = false,
                };
            }
            var pro = new Payment
            {
                CustomerId = getcurrentcustomer.Id,
                DateCreated = DateTime.Now,
                Amount = product.Price,
                PaymentMethod = "PayStack",
                PaymentStatus = PaymentStatus.Pending,
                PaymentReference = Guid.NewGuid().ToString(),
            };

            await _paymentInterface.MakePayment(pro);
            _unitofwork.SaveChanges();

            TransactionInitializeRequest request = new()
            {
                AmountInKobo = (int)(pro.Amount *100),
                Email = getcurrentcustomer.Email,
                Reference = pro.PaymentReference,
                Currency = "NGN",
            };

            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            if (!response.Status)
            {
                return new Response<PaymentResponseModel>
                {
                    Message = "Transaction Failed",
                    Status = false,
                };
            }
            pro.PaymentStatus = PaymentStatus.Complete;
            _paymentInterface.Update(pro);
            _unitofwork.SaveChanges();

            return new Response<PaymentResponseModel>
            {
                Message = "Payment Successful",
                Status = true,
            };
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

        public async Task<Response<ICollection<PaymentResponseModel>>> GetAllCompletedPayment()
        {
            var payment = await _paymentInterface.GetAllCompletedPayment(p => p.PaymentStatus == PaymentStatus.Complete);
            if (payment == null)
            {
                return new Response<ICollection<PaymentResponseModel>>
                {
                    Message = "No complete payment yet",
                    Status = false,
                };
            }
            var completedPayment = payment.Select(x => new PaymentResponseModel
            {
                Amount = x.Amount,
                DateCreated = x.DateCreated,
                CustomerId = x.CustomerId,
                PaymentStatus = PaymentStatus.Complete,
            }).ToList();

            return new Response<ICollection<PaymentResponseModel>>
            {
                Message = "List of completed payment",
                Status = true,
                Value = completedPayment,
            };
        }

        public async Task<Response<ICollection<PaymentResponseModel>>> GetAllFailedPayment()
        {
            var payments = await _paymentInterface.GetAllFailedPayment(p => p.PaymentStatus == PaymentStatus.Failed);
            if (payments == null)
            {
                return new Response<ICollection<PaymentResponseModel>>
                {
                    Message = "No failed payment yet",
                    Status = false,
                };
            }
            var failedPayment = payments.Select(x => new PaymentResponseModel
            {
                Amount = x.Amount,
                DateCreated = x.DateCreated,
                CustomerId = x.CustomerId,
                PaymentStatus = PaymentStatus.Failed,
            }).ToList();

            return new Response<ICollection<PaymentResponseModel>>
            {
                Message = "List of failed payment",
                Status = true,
                Value = failedPayment,
            };
        }

        public async Task<Response<ICollection<PaymentResponseModel>>> GetAllPendingPayment()
        {
            var pending = await _paymentInterface.GetAllPendingPayment(p => p.PaymentStatus == PaymentStatus.Pending);
            if (pending == null)
            {
                return new Response<ICollection<PaymentResponseModel>>
                {
                    Message = "No pending payment",
                    Status = false,
                };
            }
            var pendingPayment = pending.Select(x => new PaymentResponseModel
            {
                Amount = x.Amount,
                DateCreated = x.DateCreated,
                CustomerId = x.CustomerId,
                PaymentStatus = PaymentStatus.Pending,
            }).ToList();

            return new Response<ICollection<PaymentResponseModel>>
            {
                Message = "List of pending payment",
                Status = true,
                Value = pendingPayment,
            };
        }

    }
}