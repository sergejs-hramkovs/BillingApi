using Billing.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController
    {
        public BillingController()
        {

        }

        [HttpPost("processOrder")]
        public async Task<IActionResult> ProcessOrder([FromBody] OrderInputDto orderInput)
        {

        }
    }
}
