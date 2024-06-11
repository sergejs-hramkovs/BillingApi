using API.Validators;
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
        private readonly OrderInputValidator _orderInputValidator;

        public BillingController(IBillingService billingService, OrderInputValidator orderInputValidator)
        {
            _billingService = billingService;
            _orderInputValidator = orderInputValidator;
        }

        [HttpPost("processOrder")]
        public IActionResult ProcessOrder([FromForm] OrderInputDto orderInput)
        {
            var validationResult = _orderInputValidator.Validate(orderInput);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = _billingService.ProcessOrder(orderInput);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, result.Error);
        }
    }
}
