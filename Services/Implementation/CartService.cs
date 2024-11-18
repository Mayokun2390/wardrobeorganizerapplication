// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;
using static WardrobeOrganizerApp.Dtos.ProductItemDto;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartInterface _cartInterface;
        private readonly IProductInterface _productInterface;
        private readonly ICurrentUser _currentUser;
        private readonly IUserInterface _userInterface;
        private readonly IUnitOfWork _unitofwork;
        private readonly ICustomerInterface _customerInterface;
        public CartService(ICartInterface cartInterface, IProductInterface productInterface, IUnitOfWork unitofwork, ICurrentUser currentUser, IUserInterface userInterface, ICustomerInterface customerInterface)
        {
            _cartInterface = cartInterface;
            _productInterface = productInterface;
            _currentUser = currentUser;
            _unitofwork = unitofwork;
            _userInterface = userInterface;
            _customerInterface = customerInterface;
        }

        public async Task<Response<AddToCartResponseModel>> AddToCart(CreateOrderRequest model)
        {
            var product = await _productInterface.GetById(model.ProductId);
            if (product == null) throw new Exception("Product not found");

            var cart = await _cartInterface.GetCartByUserId(model.UserId) ?? new Cart { UserId = model.UserId };

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == model.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += model.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    Price = product.Price
                });
            }

            if (cart.Id == null)
            {
                await _cartInterface.AddCart(cart);
            }
            else
            {
                await _cartInterface.UpdateCart(cart);
            }
            return new Response<AddToCartResponseModel>
            {
                Message = "Cart Created Successfully",
                Status = true,
            };
        }

        public async Task<Response<AddToCartResponseModel>> DeleteCartByUserId(Guid userId)
        {
            var cart = await _cartInterface.GetCartByUserId(userId);
            if (cart != null)
            {
                await _cartInterface.DeleteCartItems(cart.Id);
            }
            return new Response<AddToCartResponseModel>
            {
                Message = "Cart Deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<ICollection<AddToCartResponseModel>>> GetAllCarts()
        {
            var cart = await _cartInterface.GetAllCarts();
            var getCarts = cart.Select(x => new AddToCartResponseModel
            {
               
            }).ToList();

            return new Response<ICollection<AddToCartResponseModel>>
            {
                Value = getCarts,
                Message = "List of Carts",
                Status = true,
            };
        }

        public async Task<Response<AddToCartResponseModel>> GetCartByUserId(Guid userId)
        {
            var cart = await _cartInterface.GetCartByUserId(userId);
            if (cart == null)
            {
                return new Response<AddToCartResponseModel>
                {
                    Message = "cart not found",
                    Status = false,
                };
            }
            var getCart = new AddToCartResponseModel
            {
                Items = cart.Items,
            };
            return new Response<AddToCartResponseModel>
            {
                Message = "Cart Exist",
                Status = true,
                Value = getCart,
            };

        }

        public async Task<Response<AddToCartResponseModel>> Update(CreateOrderRequest model, Guid id)
        {
            var cart = await _cartInterface.GetCartByUserId(id);
            if (cart == null)
            {
                return new Response<AddToCartResponseModel>
                {
                    Message = "Cart not found",
                    Status = false,
                };
            }
            var updateCart = new Cart();
            _cartInterface.UpdateCart(updateCart);
            _unitofwork.SaveChanges();
            return new Response<AddToCartResponseModel>
            {
                Message = "Cart Updated",
                Status = true,
            };
        }
    }
}