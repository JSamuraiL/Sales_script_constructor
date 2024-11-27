using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Interfaces;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

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
        public void TestMethod1()
        {
        }
    }
}
