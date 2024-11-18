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


        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        public CustomerController(ICustomerService customerService, IProductService productService, IOrderService orderService, IPaymentService paymentService, IUserService userService, ICartService cartService) 
        {
            _customerService = customerService;
            _productService = productService;
            _orderService = orderService;
            _paymentService = paymentService;
            _userService = userService;
            _cartService = cartService;
        }
       

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer(CustomerRequestModel request)
        {
            var customer = await _customerService.CreateCustomer(request);
            if (!customer.Status)
            {
                return BadRequest(customer.Message);
            }
            return Ok(customer.Message);
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


        [HttpGet("{email}, getcustomer")]
        public async Task<IActionResult> GetCustomer(string  email)
        {
            var get = await _customerService.Get(email);
            if (get == null)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Value);
        }

        [HttpGet("{id}, getcustomerbyid")]
        public async Task<IActionResult> GetCustomerById(Guid  id)
        {
            var getby = await _customerService.GetById(id);
            if (getby == null)
            {
                return BadRequest(getby.Message);
            }
            return Ok(getby.Value);
        }

    }
}