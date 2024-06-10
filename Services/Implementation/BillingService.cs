﻿using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Interfaces;
using Services.Interfaces;

namespace Billing.Services.Implementation
{
    public class BillingService : IBillingService
    {
        private readonly IEnumerable<IPaymentGatewayStrategy> _paymentGatewayStrategies;
        private readonly IReceiptService _receiptService;

        public BillingService(IEnumerable<IPaymentGatewayStrategy> paymentGatewayStrategies, IReceiptService receiptService)
        {
            _paymentGatewayStrategies = paymentGatewayStrategies;
            _receiptService = receiptService;
        }

        public ServiceResult ProcessOrder(OrderInputDto orderInput)
        {
            IPaymentGatewayStrategy? paymentGatewayStrategy = _paymentGatewayStrategies
                .FirstOrDefault(x => x.GatewayType == orderInput.GatewayType);

            if (paymentGatewayStrategy == null)
            {
                throw new InvalidOperationException("The required strategy wasn't found!");
            }

            ServiceResult paymentResult = paymentGatewayStrategy.ProcessPayment(orderInput);
            if (paymentResult.IsSuccess)
            {
                ServiceResult receiptResult = _receiptService.CreatePaymentReceipt(orderInput);
                if (receiptResult.IsSuccess)
                {
                    return receiptResult;
                }
            }

            return new ServiceResult(null, new ServiceResultError("Error processing order"));

        }
    }
}
