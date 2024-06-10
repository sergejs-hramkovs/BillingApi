using Billing.Data.Enums;

namespace Billing.Data.Dto
{
    public class OrderInputDto
    {
        public int OrderNumber { get; set; }

        public int UserId { get; set; }

        public float PaymentAmount { get; set; }

        public PaymentGateway GatewayType { get; set; }

        public string? OrderDescription { get; set; }
    }
}
