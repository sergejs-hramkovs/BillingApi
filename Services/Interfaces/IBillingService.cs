using Billing.Data.Dto;
using Billing.Data.Dto.Output;

namespace Services.Interfaces
{
    public interface IBillingService
    {
        /// <summary>
        /// Processes an order for a given input.
        /// </summary>
        /// <param name="orderInput">The input data for the order.</param>
        /// <returns>A ServiceResult object that contains the result of the order process.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no suitable payment gateway strategy is found for the given order input.</exception>
        ServiceResult ProcessOrder(OrderInputDto orderInput);
    }
}