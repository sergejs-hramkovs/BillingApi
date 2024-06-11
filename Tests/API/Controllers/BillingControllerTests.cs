using Billing.Data.Dto;
using Billing.Data.Dto.Output;
using BillingApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Interfaces;
using System.Net;

namespace Tests.API.Controllers
{
    [TestClass]
    public class BillingControllerTests
    {
        private readonly BillingController _billingController;
        private readonly Mock<IBillingService> _mockBillingService;
        private readonly Mock<ILogger<BillingController>> _mockLogger;

        public BillingControllerTests()
        {
            _mockBillingService = new Mock<IBillingService>();
            _mockLogger = new Mock<ILogger<BillingController>>();
            _billingController = new BillingController(_mockBillingService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void ProcessOrder_BillingServiceExecutesSucessfully_ReturnOkObjectResult()
        {
            // Arrange
            var orderInput = new OrderInputDto();

            // Expected
            var expectedServiceResult = new ServiceResult(orderInput, null);
            var expectedResult = new OkObjectResult(expectedServiceResult.Data);

            // Mocks Setup
            _mockBillingService.Setup(x => x.ProcessOrder(orderInput)).Returns(expectedServiceResult);

            // Act
            var result = _billingController.ProcessOrder(orderInput);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void ProcessOrder_BillingServiceFails_ReturnInternalServerError()
        {
            // Arrange
            var orderInput = new OrderInputDto();

            // Expected
            var expectedServiceResultError = new ServiceResultError("Error");
            var expectedServiceResult = new ServiceResult(null, expectedServiceResultError);

            // Mocks Setup
            _mockBillingService.Setup(x => x.ProcessOrder(orderInput)).Returns(expectedServiceResult);

            // Act
            var result = _billingController.ProcessOrder(orderInput);

            // Assert
            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            ((ObjectResult)result).Value.Should().BeEquivalentTo(expectedServiceResultError);
        }
    }
}
