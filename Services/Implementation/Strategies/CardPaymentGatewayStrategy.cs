using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;
using Billing.Services.Interfaces;

namespace Billing.Services.Implementation.Strategies
{
    public class CardPaymentGatewayStrategy : IPaymentGatewayStrategy
    {
        public PaymentGateway? GatewayType => PaymentGateway.Card;

        public CardPaymentGatewayStrategy()
        {

        }

        public ServiceResult ProcessPayment(OrderInputDto orderInput)
        {
            Console.WriteLine("Card");

            return new ServiceResult("Card", null);
        }
    }
}
