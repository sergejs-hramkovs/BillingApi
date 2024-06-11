using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Interfaces;
using Data.Constants;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Billing.Services.Implementation
{
    public class BillingService : IBillingService
    {
        private readonly IEnumerable<IPaymentGatewayStrategy> _paymentGatewayStrategies;
        private readonly IReceiptService _receiptService;
        private readonly ILogger<BillingService> _logger;

        public BillingService(
            IEnumerable<IPaymentGatewayStrategy> paymentGatewayStrategies,
            IReceiptService receiptService,
            ILogger<BillingService> logger)
        {
            _paymentGatewayStrategies = paymentGatewayStrategies;
            _receiptService = receiptService;
            _logger = logger;
        }

        /// <summary>
        /// Processes an order for a given input.
        /// </summary>
        /// <param name="orderInput">The input data for the order.</param>
        /// <returns>A ServiceResult object that contains the result of the order process.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no suitable payment gateway strategy is found for the given order input.</exception>
        public ServiceResult ProcessOrder(OrderInputDto orderInput)
        {
            _logger.LogInformation(
                "[{Class} - {Method}]: Processing order, OrderNumber: {Number}, UserId: {Id}",
                nameof(BillingService),
                nameof(ProcessOrder),
                orderInput.OrderNumber,
                orderInput.UserId);

            IPaymentGatewayStrategy? paymentGatewayStrategy = _paymentGatewayStrategies
                .FirstOrDefault(x => x.GatewayType == orderInput.GatewayType);

            if (paymentGatewayStrategy == null)
            {
                _logger.LogInformation(
                    "[{Class} - {Method}]: Error Processing order, OrderNumber: {Number}, UserId: {Id}, Error: {Error}",
                    nameof(BillingService),
                    nameof(ProcessOrder),
                    orderInput.OrderNumber,
                    orderInput.UserId,
                    ErrorMessages.StrategyNotFound);

                throw new InvalidOperationException(ErrorMessages.StrategyNotFound);
            }

            ServiceResult paymentResult = paymentGatewayStrategy.ProcessPayment(orderInput);
            if (paymentResult.IsSuccess)
            {
                ServiceResult receiptResult = _receiptService.CreatePaymentReceipt(orderInput, paymentResult);
                if (receiptResult.IsSuccess)
                {
                    return receiptResult;
                }
            }

            _logger.LogInformation(
                "[{Class} - {Method}]: Error Processing order, OrderNumber: {Number}, UserId: {Id}, Error: {Error}",
                nameof(BillingService),
                nameof(ProcessOrder),
                orderInput.OrderNumber,
                orderInput.UserId,
                ErrorMessages.OrderProcessingError);

            return new ServiceResult(null, new ServiceResultError(ErrorMessages.OrderProcessingError));
        }
    }
}
