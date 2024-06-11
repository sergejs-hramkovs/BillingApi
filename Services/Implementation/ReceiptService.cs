using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Billing.Services.Implementation
{
    public class ReceiptService : IReceiptService
    {
        private readonly ILogger<ReceiptService> _logger;

        public ReceiptService(ILogger<ReceiptService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a payment receipt for a given order and payment result.
        /// </summary>
        /// <param name="orderInput">The input data for the order.</param>
        /// <param name="paymentResult">The result of the payment process.</param>
        /// <returns>A ServiceResult object that contains the payment receipt as a string.</returns>
        public ServiceResult CreatePaymentReceipt(OrderInputDto orderInput, ServiceResult paymentResult)
        {
            _logger.LogInformation(
                "[{Class} - {Method}]: Creating a payment receipt, OrderNumber: {Number}, UserId: {Id}",
                nameof(ReceiptService),
                nameof(CreatePaymentReceipt),
                orderInput.OrderNumber,
                orderInput.UserId);

            StringBuilder sb = new();
            sb.AppendLine("######");
            sb.AppendLine(paymentResult.Data?.ToString());
            sb.AppendLine($"OrderNumber: {orderInput.OrderNumber}");
            sb.AppendLine($"UserId: {orderInput.UserId}");
            sb.AppendLine($"Amount: {orderInput.PaymentAmount}");
            sb.AppendLine($"Type: {orderInput.GatewayType}");
            sb.AppendLine($"Description: {orderInput.OrderDescription ?? "N/A"}");
            sb.AppendLine("######");

            var receiptResult = new ServiceResult(sb.ToString(), null);

            _logger.LogInformation(
                "[{Class} - {Method}]: Payment receipt has been successfully created, OrderNumber: {Number}, UserId: {Id}",
                nameof(ReceiptService),
                nameof(CreatePaymentReceipt),
                orderInput.OrderNumber,
                orderInput.UserId);

            return receiptResult;
        }
    }
}
