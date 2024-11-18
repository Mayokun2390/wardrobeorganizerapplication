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
        private readonly IUserInterface _userInterface;
        private readonly ICartInterface _cartInterface;
        public OrderService(IOrderInterface orderInterface, IUnitOfWork unitofwork, IProductInterface productInterface, ICustomerInterface customerInterface, ICurrentUser currentUser, IPaymentInterface paymentInterface, IUserInterface userInterface, ICartInterface cartInterface)
        {
            _orderInterface = orderInterface;
            _unitofwork = unitofwork;
            _productInterface = productInterface;
            _customerInterface = customerInterface;
            _currentUser = currentUser;
            _paymentInterface = paymentInterface;
            _userInterface = userInterface;
            _cartInterface = cartInterface;
        }

        public async Task<Response<OrderResponseModel>> ApproveOrder(Guid id)
        {
            var user = await _customerInterface.GetCustomerByEmail(a => a.Email == _currentUser.GetCurrentUser());
            if(user == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = " This user is not authorized to perform this function",
                    Status = false,
                };
            }
            var order = await _orderInterface.ApprovedOrder(id);
            if (order == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not Approved",
                    Status = false,
                };
            }

            order.OrderStatus = OrderStatus.IsApproved;
            _orderInterface.Update(order);
            _unitofwork.SaveChanges();

            return new Response<OrderResponseModel>
            {
                Message = "Order Approved",
                Status = true,
            };
        }

        public async Task<Response<OrderResponseModel>> CreateOrder(OrderRequestModel model)
        {
            var user = await _customerInterface.GetCustomerByEmail(m => m.Email == _currentUser.GetCurrentUser());
            if(user == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "User not found",
                    Status = false,
                };
            }
            var cart = await _cartInterface.GetCartByUserId(model.UserId);
            if (cart == null || !cart.Items.Any())
                throw new Exception("Cart is empty");

            var order = new Order
            {
                UserId = model.UserId,
                CustomerId = user.CustomerId,
                Quantity = model.Quantity,
                TotalAmount = cart.Items.Sum(i => i.Price * i.Quantity),
                OrderDate = DateTime.UtcNow,
                OrderStatus = OrderStatus.Pending,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price,
                }).ToList()
            };

            await _orderInterface.MakeOrder(order);
            _unitofwork.SaveChanges();
            return new Response<OrderResponseModel>
            {
                Message = "Order made Successfully",
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
            var disapprove = await _orderInterface.NotApprovedOrder(id);
            if (disapprove == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "Order not Found",
                    Status = false,
                };
            }

            disapprove.OrderStatus = OrderStatus.NotApproved;
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
            var getorders = await _orderInterface.GetAllApprovedOrder(x => x.OrderStatus == OrderStatus.IsApproved);
            var getApprovedOrders = getorders.Select(x => new OrderResponseModel{  
            OrderDate = x.OrderDate,
            TotalAmount = x.TotalAmount,
            Quantity = x.Quantity,
            OrderStatus = x.OrderStatus,
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
            var getOrders = await _orderInterface.GetAllDisApprovedOrder(x => x.OrderStatus == OrderStatus.NotApproved);
            var getDisapprovedOrders = getOrders.Select(x => new OrderResponseModel{
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                Quantity = x.Quantity,
                OrderStatus = x.OrderStatus,
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
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                Quantity = x.Quantity,
                OrderStatus = x.OrderStatus,
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
                TotalAmount = getApprove.TotalAmount,
                Quantity = getApprove.Quantity,
                OrderDate = getApprove.OrderDate,
                OrderStatus = getApprove.OrderStatus,
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
                TotalAmount = getDisapprove.TotalAmount,
                Quantity = getDisapprove.Quantity,
                OrderDate = getDisapprove.OrderDate,
                OrderStatus = getDisapprove.OrderStatus,
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
                TotalAmount = order.TotalAmount,
                Quantity = order.Quantity,
                OrderStatus = order.OrderStatus,
            };
            return new Response<OrderResponseModel>
            {
                Message = "Order Exist",
                Status = true,
                Value = getOrder,
            };

            
        }

        public async Task<Response<OrderResponseModel>> IsDelivered(Guid id)
        {
            var CheckIsApproved = await _orderInterface.GetOrderById(id);
            if (CheckIsApproved == null)
            {
                return new Response<OrderResponseModel>
                {
                    Message = "ApprovedOrder not found",
                    Status =  false,
                };
            }
            var getDelivered = new OrderResponseModel
            {
                TotalAmount = CheckIsApproved.TotalAmount,
                Quantity = CheckIsApproved.Quantity,
                OrderDate = CheckIsApproved.OrderDate,
                OrderStatus = OrderStatus.IsApproved,
            };
            return new Response<OrderResponseModel>
            {
                Message = "Order Delivered",
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