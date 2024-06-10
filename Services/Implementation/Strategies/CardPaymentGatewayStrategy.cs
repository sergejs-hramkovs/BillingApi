using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;
using Billing.Services.Interfaces;
using Services.Interfaces;

namespace Billing.Services.Implementation.Strategies
{
    public class CardPaymentGatewayStrategy : IPaymentGatewayStrategy
    {
        private readonly IUserCheckingService _userCheckingService;
        private readonly IReceiptService _receiptService;

        public PaymentGateway? GatewayType => PaymentGateway.Card;

        public CardPaymentGatewayStrategy(IUserCheckingService userCheckingService, IReceiptService receiptService)
        {
            _userCheckingService = userCheckingService;
            _receiptService = receiptService;
        }

        public ServiceResult ProcessPayment(OrderInputDto orderInput)
        {
            if (_userCheckingService.IsUserValid(orderInput.UserId))
            {
                ServiceResult receiptResult = _receiptService.CreatePaymentReceipt(orderInput);
                return receiptResult;
            }

            return new ServiceResult(null, new ServiceResultError("Error processing payment"));
        }
    }
}
