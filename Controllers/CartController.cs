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
            var delete = await _cartService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CartRequestModel model)
        {
            var update = await _cartService.Update(model);
            if (!update.Status == false)
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            var getall = await _cartService.GetAll();
            if (!getall.Status == false)
            {
                return BadRequest(getall.Message);
            }
            return Ok(getall.Message);
        }

        [HttpPost("Add-Cart")]
        public async Task<IActionResult> AddToCart(CartRequestModel model)
        {
            var add = await _cartService.AddToCart(model);
            if (!add.Status == false)
            {
                return BadRequest(add.Message);
            }
            return Ok(add.Message);
        }

        [HttpPost("Create_Cart")]
        public async Task<IActionResult> CreateCart(CartRequestModel model)
        {
            var create = await _cartService.Create(model);
            if (!create.Status == false)
            {
                return BadRequest(create.Message);
            }
            return Ok(create.Message);
        }
    }
}