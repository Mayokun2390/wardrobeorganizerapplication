using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            if (!order.Status)
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
            if (orders == null)
            {
                return BadRequest(orders.Message);
            }
            return Ok(orders.Value);
        }

        [HttpGet("{id}, getalldisapproveorder")]

        public async Task<IActionResult> GetAllDisApproveOrder()
        {
            var disorder = await _orderService.GetAllDisApprovedOrders();
            if (disorder == null)
            {
                return BadRequest(disorder.Message);
            }
            return Ok(disorder.Value);
        }

        [HttpGet("{id}, getapprovedorder")]

        public async Task<IActionResult> GetApproveOrder(Guid id)
        {
            var getapprove = await _orderService.GetApprovedOrder(id);
            if (getapprove == null)
            {
                return BadRequest(getapprove.Message);
            }
            return Ok(getapprove.Value);
        }
        [HttpGet("{id}, GetDisapproveOrder")]

        public async Task<IActionResult> GetDisapproveOrder(Guid id)
        {
            var disapprove = await _orderService.GetApprovedOrder(id);
            if (disapprove == null)
            {
                return BadRequest(disapprove.Message);
            }
            return Ok(disapprove.Value);
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
            if (get == null)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Value);
        }

        // [Authorize ("customer")]
        [HttpPost ]
        public async Task<IActionResult> MakeOrder(OrderRequestModel model)
        {
            var makeorder = await _orderService.CreateOrder(model);
            if (!makeorder.Status)
            {
                return BadRequest(makeorder.Message);
            }
            return Ok(makeorder.Message);
        }


        [HttpGet("{id}, getdeliveredorder")]
        public async Task<IActionResult> GetDeliveredOrder(Guid id)
        {
            var get = await _orderService.IsDelivered(id);
            if (get == null)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Value);
        }
    }
}


