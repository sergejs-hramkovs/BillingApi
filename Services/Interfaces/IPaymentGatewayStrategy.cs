using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;

namespace Billing.Services.Interfaces
{
    public interface IPaymentGatewayStrategy
    {
        /// <summary>
        /// The type of payment gateway.
        /// </summary>
        PaymentGateway? GatewayType { get; }

        /// <summary>
        /// Processes a payment for a given order.
        /// </summary>
        /// <param name="orderInput">The input data for the order.</param>
        /// <returns>A ServiceResult object that contains the result of the payment process.</returns>
        ServiceResult ProcessPayment(OrderInputDto orderInput);
    }
}
