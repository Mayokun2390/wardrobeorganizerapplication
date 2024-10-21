using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartInterface _cartInterface;
        private readonly IProductInterface _productInterface;
        private readonly IUnitOfWork _unitofwork;
        public CartService(ICartInterface cartInterface, IProductInterface productInterface, IUnitOfWork unitofwork)
        {
            _cartInterface = cartInterface;
            _productInterface = productInterface;
            _unitofwork = unitofwork;
        }
        public async Task<Response<CartResponseModel>> AddToCart(CartRequestModel model)
        {
            var getCart = await _cartInterface.GetCart(model.Id);
            if (getCart == null)
            {
                return new Response<CartResponseModel>
                {
                    Message = "Cart not found",
                    Status = false,
                };
            }
            var cart = new Cart
            {
                Id = model.Id,
                NameOfProduct = model.NameOfProduct,
                Quantity = model.Quantity, 
                TotalPrice = model.TotalPrice,
            };
            
        }

        public async Task<Response<CartResponseModel>> Delete(Guid id)
        {
             var carts = await _cartInterface.GetCart(id);
            if (carts == null)
            {
                return new Response<CartResponseModel>
                {
                    Message = "Cart not found",
                    Status = false,
                };
            }
            _cartInterface.Delete(carts);
            _unitofwork.SaveChanges();
            return new Response<CartResponseModel>
            {
                Message = "Cart deleted Successfully",
                Status = true,
            };
        }

        public async Task<Response<ICollection<CartResponseModel>>> GetAll()
        {
            var cart = await _cartInterface.GetAllCarts();
            var getCarts = cart.Select(x => new CartResponseModel{  
                Quantity = x.Quantity,
                TotalPrice = x.TotalPrice,
                NameOfProduct = x.NameOfProduct,
            }).ToList();

            return new Response<ICollection<CartResponseModel>>
            {
                Value = getCarts,
                Message = "List of Carts",
                Status = true,
            };
        }

        public async Task<Response<CartResponseModel>> Update(CartRequestModel model)
        {
            var carts = await _cartInterface.GetCart(model.ProductId);
            if (carts == null)
            {
                return new Response<CartResponseModel>
                {
                    Message = "Cart not found",
                    Status = false,
                };
            }
            var updateCart = new Cart();
            carts.Quantity = model.Quantity;
            carts.TotalPrice = model.TotalPrice;
            carts.NameOfProduct = model.NameOfProduct;
            _cartInterface.Update(updateCart);
            _unitofwork.SaveChanges();
            return new Response<CartResponseModel>
            {
                Message = "Cart Updated",
                Status = true,
            };
        }
    }
}