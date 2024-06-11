using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;
using Billing.Services.Interfaces;
using Data.Constants;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Billing.Services.Implementation.Strategies
{
    public class CryptoPaymentGatewayStrategy : IPaymentGatewayStrategy
    {
        private readonly IUserCheckingService _userCheckingService;
        private readonly ILogger<CryptoPaymentGatewayStrategy> _logger;

        /// <summary>
        /// The type of payment gateway.
        /// </summary>
        public PaymentGateway? GatewayType => PaymentGateway.Crypto;

        public CryptoPaymentGatewayStrategy(IUserCheckingService userCheckingService, ILogger<CryptoPaymentGatewayStrategy> logger)
        {
            _userCheckingService = userCheckingService;
            _logger = logger;
        }

        /// <summary>
        /// Processes a crypto payment for a given order.
        /// </summary>
        /// <param name="orderInput">The input data for the order.</param>
        /// <returns>A ServiceResult object that contains the result of the payment process.</returns>
        public ServiceResult ProcessPayment(OrderInputDto orderInput)
        {
            _logger.LogInformation(
                "[{Class} - {Method}]: Processing payment, OrderNumber: {Number}, UserId: {Id}",
                nameof(CryptoPaymentGatewayStrategy),
                nameof(ProcessPayment),
                orderInput.OrderNumber,
                orderInput.UserId);

            if (_userCheckingService.IsUserValid(orderInput.UserId))
            {
                return new ServiceResult(SuccessMessages.CryptoPaymentSuccess, null);
            }

            _logger.LogError(
                "[{Class} - {Method}]: Error processing payment, OrderNumber: {Number}, UserId: {id}, Error: {error}",
                nameof(CryptoPaymentGatewayStrategy),
                nameof(ProcessPayment),
                orderInput.OrderNumber,
                orderInput.UserId,
                ErrorMessages.PaymentProcessingError);

            return new ServiceResult(null, new ServiceResultError(ErrorMessages.PaymentProcessingError));
        }
    }
}
