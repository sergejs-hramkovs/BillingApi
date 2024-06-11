using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using Billing.Services.Implementation.Strategies;
using Data.Constants;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Interfaces;

namespace Tests.Services.Strategies
{
    [TestClass]
    public class CryptoPaymentGatewayStrategyTests
    {
        private readonly CryptoPaymentGatewayStrategy _cryptoPaymentGatewayStrategy;
        private readonly Mock<IUserCheckingService> _mockUserCheckingService;
        private readonly Mock<ILogger<CryptoPaymentGatewayStrategy>> _mockLogger;

        public CryptoPaymentGatewayStrategyTests()
        {
            _mockUserCheckingService = new Mock<IUserCheckingService>();
            _mockLogger = new Mock<ILogger<CryptoPaymentGatewayStrategy>>();
            _cryptoPaymentGatewayStrategy = new CryptoPaymentGatewayStrategy(_mockUserCheckingService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void ProcessPayment_UserIsValid_ReturnsResultWithSucess()
        {
            // Arrange
            var orderInput = new OrderInputDto
            {
                UserId = 123
            };

            // Mocks Setup
            _mockUserCheckingService.Setup(x => x.IsUserValid(orderInput.UserId)).Returns(true);

            // Expected
            var expectedResult = new ServiceResult(SuccessMessages.CryptoPaymentSuccess, null);

            // Act
            var result = _cryptoPaymentGatewayStrategy.ProcessPayment(orderInput);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void ProcessPayment_UserIsNotValid_ReturnsResultWithError()
        {
            // Arrange
            var orderInput = new OrderInputDto
            {
                UserId = 123
            };

            // Mocks Setup
            _mockUserCheckingService.Setup(x => x.IsUserValid(orderInput.UserId)).Returns(false);

            // Expected
            var expectedResult = new ServiceResult(null, new ServiceResultError(ErrorMessages.PaymentProcessingError));

            // Act
            var result = _cryptoPaymentGatewayStrategy.ProcessPayment(orderInput);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
