using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpDelete("Delete")]
        public async  Task<IActionResult> Delete(Guid id)
        {
            var delete = await _cartService.DeleteCartByUserId(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CreateOrderRequest model, Guid id)
        {
            var update = await _cartService.Update(model, id);
            if (!update.Status == false)
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            var getall = await _cartService.GetAllCarts();
            if (getall == null)
            {
                return BadRequest(getall.Message);
            }
            return Ok(getall.Value);
        }

        // [HttpPost("Add-Cart")]
        // public async Task<IActionResult> AddToCart(CreateOrderRequest model)
        // {
        //     var add = await _cartService.AddToCart(model);
        //     if (!add.Status)
        //     {
        //         return BadRequest(add.Message);
        //     }
        //     return Ok(add.Message);
        // }

        [HttpPost("Create_Cart")]
        public async Task<IActionResult> CreateCart(CreateOrderRequest model)
        {
            var create = await _cartService.AddToCart(model);
            if (!create.Status)
            {
                return BadRequest(create.Message);
            }
            return Ok(create.Message);
        }
    }
}