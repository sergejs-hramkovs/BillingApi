using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Interfaces;
using System.Text;

namespace Billing.Services.Implementation
{
    public class ReceiptService : IReceiptService
    {
        public ReceiptService()
        {

        }

        public ServiceResult CreatePaymentReceipt(OrderInputDto orderInput)
        {
            StringBuilder sb = new();
            sb.AppendLine("######");
            sb.AppendLine($"OrderNumber: {orderInput.OrderNumber}");
            sb.AppendLine($"UserId: {orderInput.UserId}");
            sb.AppendLine($"Amount: {orderInput.PaymentAmount}");
            sb.AppendLine($"Type: {orderInput.GatewayType}");
            sb.AppendLine($"Description: {orderInput.OrderDescription ?? "N/A"}");
            sb.AppendLine("######");

            var receiptResult = new ServiceResult(sb.ToString(), null);
            return receiptResult;
        }
    }
}
