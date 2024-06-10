namespace Billing.Data.Dto
{
    public class OrderInputDto
    {
        public int OrderNumber { get; set; }

        public int UserId { get; set; }

        public float PaymentAmount { get; set; }

        public int PaymentGatewayId { get; set; }

        public string? OrderDescription { get; set; }
    }
}
