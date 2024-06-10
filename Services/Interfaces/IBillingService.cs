using Billing.Data.Dto;
using Billing.Data.Dto.Output;

namespace Services.Interfaces
{
    public interface IBillingService
    {
        ServiceResult ProcessOrder(OrderInputDto orderInput);
    }
}