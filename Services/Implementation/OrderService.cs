using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderInterface _orderInterface;
        private readonly IUnitOfWork _unitofwork;
        private readonly IProductInterface _productInterface;
        private readonly ICustomerInterface _customerInterface;
        private readonly ICurrentUser _currentUser;
        private readonly IPaymentInterface _paymentInterface;
        public OrderService(IOrderInterface orderInterface, IUnitOfWork unitofwork, IProductInterface productInterface, ICustomerInterface customerInterface, ICurrentUser currentUser, IPaymentInterface paymentInterface)
        {
            _orderInterface = orderInterface;
            _unitofwork = unitofwork;
            _productInterface = productInterface;
            _customerInterface = customerInterface;
            _currentUser = currentUser;
            _paymentInterface = paymentInterface;
        }
        public async Task<Response<OrderResponseModel>> ApproveOrder(Guid id)
        {
            var order = await _orderInterface.GetOrderById(id);
            if (order == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not Approved",
                    Status = false,
                };
            }

            order.IsApproved = true;    
            order.Status = Status.IsDelivered;
            _orderInterface.Update(order);
            _unitofwork.SaveChanges();

            return new Response<OrderResponseModel>
            {
                Message = "Order Approved",
                Status = true,
            };
        }

        public async Task<Response<OrderResponseModel>> Delete(Guid id)
        {
            var order = await _orderInterface.GetOrderById(id);
            if (order == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }
            _orderInterface.Delete(order);
            _unitofwork.SaveChanges();
            return new Response<OrderResponseModel>
            {
                Message = "Order deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<OrderResponseModel>> DisApproved(Guid id)
        {
            var disapprove = await _orderInterface.GetOrderById(id);
            if (disapprove == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not Found",
                    Status = false,
                };
            }

            disapprove.IsApproved = false;
            disapprove.Status = Status.Pending;
            _orderInterface.Update(disapprove);
            _unitofwork.SaveChanges();

            return new Response<OrderResponseModel>
            {
                Message = "Oder not Approved",
                Status = false,
            };
        }

        public async Task<Response<ICollection<OrderResponseModel>>> GetAllApprovedOrders()
        {
            var getorders = await _orderInterface.GetAllApprovedOrder(x => x.IsApproved == true);
            var getApprovedOrders = getorders.Select(x => new OrderResponseModel{  
            DateTime = x.DateTime,
            TotalPrice = x.TotalPrice,
            Quantity = x.Quantity,
            Status = Status.IsDelivered,
            }).ToList();

            return new Response<ICollection<OrderResponseModel>>
            {
                Value = getApprovedOrders,
                Message = "List of ApprovedOrders",
                Status = true,
            };
        }

        public async Task<Response<ICollection<OrderResponseModel>>> GetAllDisApprovedOrders()
        {
            var getOrders = await _orderInterface.GetAllDisApprovedOrder(x => x.IsApproved == false);
            var getDisapprovedOrders = getOrders.Select(x => new OrderResponseModel{
                DateTime = x.DateTime,
                TotalPrice = x.TotalPrice,
                Quantity = x.Quantity,
                Status = Status.Pending,
            }).ToList();

            return new Response<ICollection<OrderResponseModel>>
            {
                Value = getDisapprovedOrders,
                Message = "List of DisapprovedOrders",
                Status = false,
            };
        }

        public async Task<Response<ICollection<OrderResponseModel>>> GetAllOrders()
        {
            var orders = await _orderInterface.GetAllOrder();
            var getOrders = orders.Select(x => new OrderResponseModel{  
                DateTime = x.DateTime,
                TotalPrice = x.TotalPrice,
                Quantity = x.Quantity,
                Status = Status.IsDelivered,
            }).ToList();

            return new Response<ICollection<OrderResponseModel>>
            {
                Value = getOrders,
                Message = "List of Orders",
                Status = true,
            };
        }

        public async Task<Response<OrderResponseModel>> GetApprovedOrder(Guid id)
        {
            var getApprove = await _orderInterface.GetOrderById(id);
            if (getApprove == null)
            {
                return new  Response<OrderResponseModel>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }
            var getOrder = new OrderResponseModel
            {
                TotalPrice = getApprove.TotalPrice,
                Quantity = getApprove.Quantity,
                DateTime = getApprove.DateTime,
                Status = Status.IsDelivered,
            };
            return new Response<OrderResponseModel>
            {
                Message = "Order Exist",
                Status = true,
                Value = getOrder,
            };
        }

        public async Task<Response<OrderResponseModel>> GetDisApprovedOrder(Guid id)
        {
            var getDisapprove = await _orderInterface.GetOrderById(id);
            if (getDisapprove == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }
            var getOrder = new OrderResponseModel
            {
                TotalPrice = getDisapprove.TotalPrice,
                Quantity = getDisapprove.Quantity,
                DateTime = getDisapprove.DateTime,
                Status = Status.Pending
            };
            return new Response<OrderResponseModel>
            {
                Message = "Order not Approved",
                Status = false,
            };
        }

        public async Task<Response<OrderResponseModel>> GetOrder(Guid id)
        {
            var order = await _orderInterface.GetOrderById(id);
            if (order == null)
            {
                return new  Response<OrderResponseModel>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }
            var getOrder = new OrderResponseModel
            {
                TotalPrice = order.TotalPrice,
                Quantity = order.Quantity,
                DateTime = order.DateTime,
                Status = Status.IsDelivered,
            };
            return new Response<OrderResponseModel>
            {
                Message = "Order Exist",
                Status = true,
                Value = getOrder,
            };
        }

        public async Task<Response<OrderResponseModel>> MakeOrder(OrderRequestModel model)
        {
            var customer = await _customerInterface.GetCustomerByEmail(m => m.Email == _currentUser.GetCurrentUser());
            var product = await _productInterface.GetById(model.Id);

            if (product == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }

            decimal totalPrice = product.Price * model.Quantity;
            if (model.Quantity < model.Quantity)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Quantity must be greater than 0",
                    Status = false,
                };
            }

            product.Quantity -= model.Quantity;
        
            _productInterface.Update(product);

            var orders = new Order
            {
                DateTime = DateTime.UtcNow,
                Quantity = product.Quantity,
                ProductId = product.Id,
                CustomerId = customer.Id,
            };
            await _orderInterface.MakeOrder(orders);
            var orderProduct = new OrderProduct
            {
                OrderId = orders.Id,
                ProductId = product.Id,
            };
            product.OrderProducts.Add(orderProduct);
            return new Response<OrderResponseModel>
            {
                Message = "Order created Successfully",
                Status = true,
            };
        }

        public async Task<Response<OrderResponseModel>> Update(OrderRequestModel model, Guid id)
        {
            var order = await _orderInterface.GetOrderById(id);
            if (order == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not found",
                    Status = false,
                };
            }
            var updateOrder = new Order();
            order.Quantity = model.Quantity;
            _orderInterface.Update(updateOrder);
            _unitofwork.SaveChanges();
            return new Response<OrderResponseModel>
            {
                Message = "Order Updated",
                Status = true,
            };
        }
    }
}