using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;
using Billing.Services.Implementation;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Services
{
    [TestClass]
    public class ReceiptServiceTests
    {
        private readonly ReceiptService _receiptService;
        private readonly Mock<ILogger<ReceiptService>> _mockLoggger;

        public ReceiptServiceTests()
        {
            _mockLoggger = new Mock<ILogger<ReceiptService>>();
            _receiptService = new ReceiptService(_mockLoggger.Object);
        }

        [TestMethod]
        public void CreatePaymentReceipt_ReceivesData_ReturnsReceipt()
        {
            // Arrange
            var successMessage = "Success";
            var paymentResult = new ServiceResult(successMessage, null);
            var orderInput = new OrderInputDto
            {
                PaymentAmount = 10,
                GatewayType = PaymentGateway.Card,
                OrderDescription = "TEST",
                OrderNumber = 123,
                UserId = 999
            };

            // Act
            var result = _receiptService.CreatePaymentReceipt(orderInput, paymentResult);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            ((string?)result.Data).Should().NotBeNullOrEmpty();
        }
    }
}
