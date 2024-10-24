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
    public class OutfitsController : ControllerBase
    {
        private readonly IOutfitsService _outfitService;
        public OutfitsController(IOutfitsService outfitService)
        {
            _outfitService = outfitService;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _outfitService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(OutfitsRequestModel model, Guid id)
        {
            var update = await _outfitService.Update(model, id);
            if (!update.Status == false) 
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet("{id}, getalloutfits")]
        public async Task<IActionResult> GetAllOutfits()
        {
            var getall = await _outfitService.GetAllOutfits();
            if (!getall.Status == false)
            {
                return BadRequest(getall.Message);
            }
            return Ok(getall.Message);
        }

        [HttpGet("{id}, GetOutfit")]
        public async Task<IActionResult> GetOutfit(Guid id)
        {
            var get = await _outfitService.Get(id);
            if (!get.Status == false)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OutfitsRequestModel model)
        {
            var create = await _outfitService.Create(model);
            if (!create.Status == false)
            {
                return BadRequest(create.Message);
            }
            return Ok(create.Message);
        }
    }
}