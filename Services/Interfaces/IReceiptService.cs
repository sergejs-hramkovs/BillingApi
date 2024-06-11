using Billing.Data.Dto;
using Billing.Data.Dto.Output;

namespace Billing.Services.Interfaces
{
    public interface IReceiptService
    {
        /// <summary>
        /// Creates a payment receipt for a given order and payment result.
        /// </summary>
        /// <param name="orderInput">The input data for the order.</param>
        /// <param name="paymentResult">The result of the payment process.</param>
        /// <returns>A ServiceResult object that contains the payment receipt as a string.</returns>
        ServiceResult CreatePaymentReceipt(OrderInputDto orderInput, ServiceResult paymentResult);
    }
}