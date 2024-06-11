using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Data.Enums;
using Billing.Services.Implementation;
using Billing.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Services
{
    [TestClass]
    public class BillingServiceTests
    {
        private BillingService _billingService;
        private IEnumerable<IPaymentGatewayStrategy> _paymentGatewayStrategies;
        private readonly Mock<IReceiptService> _mockReceiptService;
        private readonly Mock<ILogger<BillingService>> _mockLogger;

        public BillingServiceTests()
        {
            _paymentGatewayStrategies = new List<IPaymentGatewayStrategy>();
            _mockReceiptService = new Mock<IReceiptService>();
            _mockLogger = new Mock<ILogger<BillingService>>();
            _billingService = new BillingService(_paymentGatewayStrategies, _mockReceiptService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void ProcessOrder_PaymentGatewayStrategyIsNull_ThrowsException()
        {
            // Arrange
            var orderInput = new OrderInputDto();

            // Act
            Action act = () => _billingService.ProcessOrder(orderInput);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        [DataRow(PaymentGateway.Card)]
        [DataRow(PaymentGateway.Crypto)]
        public void ProcessOrder_PaymentResultIsNotSuccess_ReturnsError(PaymentGateway paymentGateway)
        {
            // Arrange
            var orderInput = new OrderInputDto
            {
                GatewayType = paymentGateway
            };

            var mockStrategy = new Mock<IPaymentGatewayStrategy>();

            // Expected
            var expectedErrorResult = new ServiceResult(null, new ServiceResultError("Error"));

            // Mocks Setup
            mockStrategy.Setup(x => x.ProcessPayment(It.IsAny<OrderInputDto>())).Returns(expectedErrorResult);
            mockStrategy.Setup(x => x.GatewayType).Returns(paymentGateway);
            _paymentGatewayStrategies = new List<IPaymentGatewayStrategy>
            {
                mockStrategy.Object
            };

            _billingService = new BillingService(_paymentGatewayStrategies, _mockReceiptService.Object, _mockLogger.Object);


            // Act
            var result = _billingService.ProcessOrder(orderInput);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [TestMethod]
        public void ProcessOrder_PaymentResultIsSuccessAndReceiptResultIsNotSuccess_ReturnsError()
        {
            // Arrange
            var orderInput = new OrderInputDto
            {
                GatewayType = PaymentGateway.Card
            };

            var mockStrategy = new Mock<IPaymentGatewayStrategy>();

            // Expected
            var expectedErrorResult = new ServiceResult(null, new ServiceResultError("Error"));

            // Mocks Setup
            mockStrategy.Setup(x => x.ProcessPayment(It.IsAny<OrderInputDto>())).Returns(expectedErrorResult);
            mockStrategy.Setup(x => x.GatewayType).Returns(PaymentGateway.Card);
            _paymentGatewayStrategies = new List<IPaymentGatewayStrategy>
            {
                mockStrategy.Object
            };

            _billingService = new BillingService(_paymentGatewayStrategies, _mockReceiptService.Object, _mockLogger.Object);

            _mockReceiptService.Setup(x => x.CreatePaymentReceipt(orderInput, It.IsAny<ServiceResult>())).Returns(expectedErrorResult);

            // Act
            var result = _billingService.ProcessOrder(orderInput);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [TestMethod]
        public void ProcessOrder_PaymentResultIsSuccessAndReceiptResultIsSuccess_ReturnsServiceResult()
        {
            // Arrange
            var orderInput = new OrderInputDto
            {
                GatewayType = PaymentGateway.Card
            };

            var mockStrategy = new Mock<IPaymentGatewayStrategy>();

            // Expected
            var expectedResult = new ServiceResult(orderInput, null);

            // Mocks Setup
            mockStrategy.Setup(x => x.ProcessPayment(It.IsAny<OrderInputDto>())).Returns(expectedResult);
            mockStrategy.Setup(x => x.GatewayType).Returns(PaymentGateway.Card);
            _paymentGatewayStrategies = new List<IPaymentGatewayStrategy>
            {
                mockStrategy.Object
            };

            _billingService = new BillingService(_paymentGatewayStrategies, _mockReceiptService.Object, _mockLogger.Object);

            _mockReceiptService.Setup(x => x.CreatePaymentReceipt(orderInput, It.IsAny<ServiceResult>())).Returns(expectedResult);

            // Act
            var result = _billingService.ProcessOrder(orderInput);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
        }
    }
}
