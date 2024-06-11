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
    public class CardPaymentGatewayStrategyTests
    {
        private readonly CardPaymentGatewayStrategy _cardPaymentGatewayStrategy;
        private readonly Mock<IUserCheckingService> _mockUserCheckingService;
        private readonly Mock<ILogger<CardPaymentGatewayStrategy>> _mockLogger;

        public CardPaymentGatewayStrategyTests()
        {
            _mockUserCheckingService = new Mock<IUserCheckingService>();
            _mockLogger = new Mock<ILogger<CardPaymentGatewayStrategy>>();
            _cardPaymentGatewayStrategy = new CardPaymentGatewayStrategy(_mockUserCheckingService.Object, _mockLogger.Object);
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
            var expectedResult = new ServiceResult(SuccessMessages.CardPaymentSuccess, null);

            // Act
            var result = _cardPaymentGatewayStrategy.ProcessPayment(orderInput);

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
            var result = _cardPaymentGatewayStrategy.ProcessPayment(orderInput);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
