using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Interfaces;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using SalesScriptConstructor.Domain.Entities;

namespace TestControllers
{
    [TestClass]
    public sealed class TestSellers
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
        public async Task GetLinkedSellers_ReturnsSellers_WhenManagerExists()
        {
            //Arrange
            var managerId = Guid.NewGuid();
            var sellers = new List<Seller> {new Seller {Id = Guid.NewGuid(), Name = "string"}};
            _mockSellersService.Setup( s => s.GetSellersByManagerId(managerId)).ReturnsAsync(sellers);

            //Act
            var result = await _controller.GetLinkedSellers(managerId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<Seller>));
            var okResult = result as IEnumerable<Seller>; 
            Assert.AreEqual(1, okResult.Count());
        }
    }
}
