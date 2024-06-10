using Billing.Data.Dto;
using Billing.Data.Dto.Output;

namespace Billing.Services.Interfaces
{
    public interface IReceiptService
    {
        ServiceResult CreatePaymentReceipt(OrderInputDto orderInput);
    }
}