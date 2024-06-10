using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;

namespace Billing.Services.Interfaces
{
    public interface IPaymentGatewayStrategy
    {
        PaymentGateway GatewayType { get; }
        ServiceResult ProcessPayment(OrderInputDto orderInput);
    }
}
