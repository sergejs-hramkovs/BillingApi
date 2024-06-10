using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Interfaces;

namespace Billing.Services.Implementation
{
    public class BillingService
    {
        private readonly IPaymentGatewayStrategy _strategy;
        private readonly IReceiptService _receiptService;

        public BillingService(IPaymentGatewayStrategy strategy, IReceiptService receiptService)
        {
            _strategy = strategy;
            _receiptService = receiptService;
        }

        public ServiceResult ProcessOrder(OrderInputDto orderInput)
        {


            ServiceResult paymentResult = _strategy.ProcessPayment(orderInput);
            bool? paymentResultData = paymentResult.Data as bool?;

            if (paymentResult.IsSuccess && paymentResultData.GetValueOrDefault())
            {
                ServiceResult receipt = _receiptService.CreatePaymentReceipt(orderInput);
                return receipt;
            }
            else
            {
                return new ServiceResult(null, new ServiceResultError("Error processing order"));
            }
        }
    }
}
