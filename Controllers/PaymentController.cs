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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpDelete]
        public async  Task<IActionResult> Delete(Guid id)
        {
            var delete = await _paymentService.Delete(id);
            if (!delete.Status == false)
            {
                return BadRequest(delete.Message);
            }
            return Ok(delete.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentRequestModel model, Guid id)
        {
            var update = await _paymentService.Update(model, id);
            if (!update.Status == false)
            {
                return BadRequest(update.Message);
            }
            return Ok(update.Message);
        }

        [HttpGet("{id}, getpayment")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var get = await _paymentService.Get(id);
            if (!get.Status == false)
            {
                return BadRequest(get.Message);
            }
            return Ok(get.Message);
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment(Guid id)
        {
            var makeorder = await _paymentService.MakePayment(id);
            if (!makeorder.Status == false)
            {
                return BadRequest(makeorder.Message);
            }
            return Ok(makeorder.Message);
        }

        [HttpGet("{id}, getallpayment")]
        public async Task<IActionResult> GetAllPaymnet()
        {
           var getall = await _paymentService.GetAllPayment();
           if (!getall.Status == false)
           {
                return BadRequest(getall.Message);
           } 
           return Ok(getall.Message);
        }

        [HttpGet("{id}, getallcompletedpayment")]
        public async Task<IActionResult> GetAllCompletedPayment()
        {
           var getallcompletepay = await _paymentService.GetAllCompletedPayment();
           if (!getallcompletepay.Status == false)
           {
                return BadRequest(getallcompletepay.Message);
           } 
           return Ok(getallcompletepay.Message);
        }

        [HttpGet("{id}, getallfailedpayment")]
        public async Task<IActionResult> GetAllfailedPayment()
        {
           var getallfailedpay = await _paymentService.GetAllFailedPayment();
           if (!getallfailedpay.Status == false)
           {
                return BadRequest(getallfailedpay.Message);
           } 
           return Ok(getallfailedpay.Message);
        }


        [HttpGet("{id}, getallpendingpayment")]
        public async Task<IActionResult> GetAllpendingPayment()
        {
           var getallpendingpay = await _paymentService.GetAllPendingPayment();
           if (!getallpendingpay.Status == false)
           {
                return BadRequest(getallpendingpay.Message);
           } 
           return Ok(getallpendingpay.Message);
        }
    }
}