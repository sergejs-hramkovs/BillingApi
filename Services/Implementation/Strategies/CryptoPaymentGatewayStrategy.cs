using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;
using Billing.Services.Interfaces;

namespace Billing.Services.Implementation.Strategies
{
    public class CryptoPaymentGatewayStrategy : IPaymentGatewayStrategy
    {
        public PaymentGateway? GatewayType => PaymentGateway.Crypto;

        public CryptoPaymentGatewayStrategy()
        {

        }

        public ServiceResult ProcessPayment(OrderInputDto orderInput)
        {
            Console.WriteLine("Crypto");

            return new ServiceResult("Crypto", null);
        }
    }
}
