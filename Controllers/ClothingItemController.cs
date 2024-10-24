using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WardrobeOrganizerApp.Dtos;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothingItemController : ControllerBase
    {
        private readonly IClothingItemService _clothingItemService;
        public ClothingItemController(IClothingItemService clothingItemService)
        {
            _clothingItemService = clothingItemService;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var delete = await _clothingItemService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClothingItemRequestModel model, Guid id)
        {
            var update = await _clothingItemService.Update(model, id);
            if (!update.Status == false)
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet("GettAll")]
        public async Task<IActionResult> GetAllClothingItems()
        {
           var getall = await _clothingItemService.GetAllClothings();
           if (!getall.Status == false)
           {
                return BadRequest(getall.Message);
           } 
           return Ok(getall.Message);
        }

        [HttpGet("{id}, GetClothingItem")]
        public async Task<IActionResult> GetClothingItems(Guid id)
        {
            var get = await _clothingItemService.Get(id);
            if (!get.Status == false)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClothingItem(ClothingItemRequestModel model)
        {
            var create = await _clothingItemService.Create(model);
            if (!create.Status == false)
            {
                return BadRequest(create.Message);
            }
            return Ok(create.Message);
        }
    }
}