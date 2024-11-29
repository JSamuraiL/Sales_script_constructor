using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;

namespace TestControllers.TestBlocks 
{ 

[TestClass]
    public class GetBlock
    {
        private Mock<ILogger<BlocksController>> _mockLogger;
        private Mock<IBlocksService> _mockBlocksService;
        private BlocksController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockBlocksService = new Mock<IBlocksService>();
            _mockLogger = new Mock<ILogger<BlocksController>>();
            _controller = new BlocksController(_mockBlocksService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Success()
        {
            //Arrange
            var block = new Block { Id = 1 };
            _mockBlocksService.Setup(s => s.GetBlockByIdAsync(block.Id)).ReturnsAsync(block);

            //Act
            var result = await _controller.GetBlock(block.Id);

            //Accert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var objectResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(block, objectResult.Value);
        }

        [TestMethod]
        public async Task NotFound()
        {
            //Arrange
            var block = new Block { Id = 1 };
            _mockBlocksService.Setup(s => s.GetBlockByIdAsync(block.Id)).Throws(new ArgumentNullException());

            //Act
            var result = await _controller.GetBlock(block.Id);

            //Accert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NotFoundObjectResult>(result);
            var objectResult = result as NotFoundObjectResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
            Assert.AreEqual("Блок с данным id не найден", objectResult.Value);
        }

        [TestMethod]
        public async Task Fatal()
        {
            //Arrange
            var block = new Block { Id = 1 };
            _mockBlocksService.Setup(s => s.GetBlockByIdAsync(block.Id)).Throws(new Exception());

            //Act
            var result = await _controller.GetBlock(block.Id);

            //Accert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
        }
    }
}