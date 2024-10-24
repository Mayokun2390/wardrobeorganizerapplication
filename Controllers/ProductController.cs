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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpDelete]
        public async  Task<IActionResult> Delete(Guid id)
        {
            var delete = await _productService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductRequestModel model, Guid id)
        {
            var update = await _productService.Update(model, id);
            if (!update.Status == false)
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet("{id} getallproduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var getall = await _productService.GetAllProducts();
            if (!getall.Status == false)
            {
                return BadRequest(getall.Message);
            }
            return Ok(getall.Message);
        }

        [HttpGet("{id} getproduct")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var get = await _productService.Get(id);
            if (!get.Status == false)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequestModel model)
        {
            var create = await _productService.CreateProduct(model);
            if (!create.Status == false)
            {
                return BadRequest(create.Message);
            }
            return Ok(create.Message);
        }
    }
}