using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOutfitsService _outfitsService;
        private readonly IClothingItemService _clothingitemService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        public CustomerController(ICustomerService customerService, IProductService productService, IOutfitsService outfitsService, IClothingItemService clothingitemService, IOrderService orderService, IPaymentService paymentService, IUserService userService, ICartService cartService) 
        {
            _customerService = customerService;
            _productService = productService;
            _outfitsService = outfitsService;
            _clothingitemService = clothingitemService;
            _orderService = orderService;
            _paymentService = paymentService;
            _userService = userService;
            _cartService = cartService;
        }
       

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer(CustomerRequestModel request)
        {
            var customer = await _customerService.CreateCustomer(request);
            if (!customer.Status == false)
            {
                return BadRequest(customer.Message);
            }
            return Ok(customer.Message);
        }
        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _userService.Login(model);
            if(!user.Status == false)
            {
                return BadRequest(user.Message);
            }
            return Ok(user.Message);
        }
        [HttpGet("{id}, viewavailableorder")]
        public async Task<IActionResult> ViewAvailableProduct(ProductResponseModel model)
        {
            var product = await _productService.GetAllProducts();
            if (!product.Status == false)
            {
                return BadRequest(product.Message);
            }
            return Ok(product.Message);
        }

        [HttpGet("{id}, viewalloutfits")]
        private async Task<IActionResult> ViewAllOutfits(OutfitsResponseModel model)
        {
            var outfit = await _outfitsService.GetAllOutfits();
            if (!outfit.Status == false)
            {
                return BadRequest(outfit.Message);
            }
            return Ok(outfit.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerProfile(CustomerRequestModel model, Guid id)
        {
            var update = await _customerService.Update(model, id);
            if (!update.Status == false)
            {
               return BadRequest(update.Message); 
            }
            return Ok(update.Message);
        }

        [HttpPost("MakeOrder")]
        public async Task<IActionResult> MakeOrder(OrderRequestModel model)
        {
            var order = await _orderService.MakeOrder(model);
            if (!order.Status == false)
            {
                return BadRequest(order.Message);
            }
            return Ok(order.Message);
        }

        [HttpPost("MakePayment")]
        public async Task<IActionResult> MakePayment(PaymentRequestModel model, Guid id)
        {
            var payment = await _paymentService.MakePayment(id);
            if(!payment.Status == false)
            {
                return BadRequest(payment.Message);
            }
            return Ok(payment.Message);
        }

        [HttpGet("{id}, viewallclothingitems")]
        public async Task<IActionResult> ViewAllClothingItems()
        {
            var clothing = await _clothingitemService.GetAllClothings();
            if (!clothing.Status == false)
            {
                return BadRequest(clothing.Message);
            }
            return Ok(clothing.Message);
        }

        [HttpGet("{id}, AddToCart")]
        public async Task<IActionResult> AddToCart(CartRequestModel model)
        {
            var cart = await _cartService.AddToCart(model);
            if (!cart.Status == false)
            {
                return BadRequest(cart.Message);
            }
            return Ok(cart.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var delete = await _customerService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }
    }
}