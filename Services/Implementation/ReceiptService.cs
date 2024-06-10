using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Interfaces;

namespace Billing.Services.Implementation
{
    public class ReceiptService : IReceiptService
    {
        public ReceiptService()
        {

        }

        public ServiceResult CreatePaymentReceipt(OrderInputDto orderInput)
        {
            return new ServiceResult(null, null);
        }
    }
}
