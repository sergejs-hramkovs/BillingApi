using Billing.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Net;

namespace BillingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpPost("processOrder")]
        public IActionResult ProcessOrder([FromForm] OrderInputDto orderInput)
        {
            var result = _billingService.ProcessOrder(orderInput);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, result.Error);
        }
    }
}
