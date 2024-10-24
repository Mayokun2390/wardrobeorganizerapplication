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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet("{id}, approveorder")]

        public async Task<IActionResult> ApprovedOrder(Guid id)
        {
            var order = await _orderService.ApproveOrder(id);
            if (!order.Status == false)
            {
                return BadRequest(order.Message);

            }
            return Ok(order.Message);
        }

        [HttpGet("{id}, disapproveorder")]
        public async Task<IActionResult> DisApproveOrder(Guid id)
        {
            var diaspprove = await _orderService.DisApproved(id);
            if (!diaspprove.Status == false)
            {
                return BadRequest(diaspprove.Message);
            }
            return Ok(diaspprove.Message);
        }

        [HttpGet("{id}, getallapproveorder")]

        public async Task<IActionResult> GetAllApprovedOrder()
        {
            var orders = await _orderService.GetAllApprovedOrders();
            if (!orders.Status == false)
            {
                return BadRequest(orders.Message);
            }
            return Ok(orders.Message);
        }

        [HttpGet("{id}, getalldisapproveorder")]

        public async Task<IActionResult> GetAllDisApproveOrder()
        {
            var disorder = await _orderService.GetAllDisApprovedOrders();
            if (!disorder.Status == false)
            {
                return BadRequest(disorder.Message);
            }
            return Ok(disorder.Message);
        }

        [HttpGet("{id}, getapprovedorder")]

        public async Task<IActionResult> GetApproveOrder(Guid id)
        {
            var getapprove = await _orderService.GetApprovedOrder(id);
            if (!getapprove.Status)
            {
                return BadRequest(getapprove.Message);
            }
            return Ok(getapprove.Message);
        }
        [HttpGet("{id}, GetDisapproveOrder")]

        public async Task<IActionResult> GetDisapproveOrder(Guid id)
        {
            var disapprove = await _orderService.GetApprovedOrder(id);
            if (!disapprove.Status == false)
            {
                return BadRequest(disapprove.Message);
            }
            return Ok(disapprove.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var delete = await _orderService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderRequestModel model, Guid id)
        {
            var update = await _orderService.Update(model, id);
            if (!update.Status == false)
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet("{id}, getorder")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var get = await _orderService.GetOrder(id);
            if (!get.Status == false)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Message);
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(OrderRequestModel model)
        {
            var makeorder = await _orderService.MakeOrder(model);
            if (!makeorder.Status == false)
            {
                return BadRequest(makeorder.Message);
            }
            return Ok(makeorder.Message);
        }
    }
}


