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
        private readonly ILogger<BillingController> _logger;

        public BillingController(
            IBillingService billingService,
            ILogger<BillingController> logger)
        {
            _billingService = billingService;
            _logger = logger;
        }

        /// <summary>
        /// Processes an order based on the provided input data.
        /// </summary>
        /// <param name="orderInput">The input data for the order to be processed.</param>
        /// <returns>A result indicating the success or failure of the order processing. </returns>
        [HttpPost("processOrder")]
        public IActionResult ProcessOrder([FromForm] OrderInputDto orderInput)
        {
            _logger.LogInformation(
                "[{Class} - {Method}]: Processing order, OrderNumber: {Number}, UserId: {Id}",
                nameof(BillingController),
                nameof(ProcessOrder),
                orderInput.OrderNumber,
                orderInput.UserId);

            var result = _billingService.ProcessOrder(orderInput);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            _logger.LogError(
                "[{Class} - {Method}]: Error processing order, OrderNumber: {Number}, UserId: {Id}, Error: {Error}",
                nameof(BillingController),
                nameof(ProcessOrder),
                orderInput.OrderNumber,
                orderInput.UserId,
                result.Error);

            return StatusCode((int)HttpStatusCode.InternalServerError, result.Error);
        }
    }
}
