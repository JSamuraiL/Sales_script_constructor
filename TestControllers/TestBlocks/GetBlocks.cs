using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Interfaces;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using SalesScriptConstructor.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TestControllers.TestBlocks
{
    [TestClass]
    public sealed class GetBlocks
    {
        private Mock<ILogger<SellersController>> _mockLogger;
        private Mock<ISellersService> _mockSellersService;
        private SellersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockSellersService = new Mock<ISellersService>();
            _mockLogger = new Mock<ILogger<SellersController>>();
            _controller = new SellersController(_mockSellersService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Success()
        {
            //Arrange
            var managerId = Guid.NewGuid();
            var sellers = new List<Seller> { new Seller { Id = Guid.NewGuid(), Name = "string" } };
            _mockSellersService.Setup(s => s.GetSellersByManagerId(managerId)).ReturnsAsync(sellers);

            //Act
            var result = await _controller.GetLinkedSellers(managerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var objectResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(sellers,objectResult.Value);
        }

        [TestMethod]
        public async Task Fatal() 
        {
            //Arrange
            var managerId = Guid.NewGuid();
            _mockSellersService.Setup(s => s.GetSellersByManagerId(managerId)).ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetLinkedSellers(managerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
        }

    }
}

