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
using SalesScriptConstructor.Domain.Interfaces.IBlocks;

namespace TestControllers.TestBlocks
{
    [TestClass]
    public sealed class GetBlocks
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
            int ScriptId = 1;
            var blocks = new List<Block> { new Block { Id = 1, ScriptId = ScriptId } };
            _mockBlocksService.Setup(s => s.GetBlocksByScriptIdAsync(ScriptId)).ReturnsAsync(blocks);

            //Act
            var result = await _controller.GetBlocks(ScriptId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var objectResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(blocks,objectResult.Value);
        }

        [TestMethod]
        public async Task Fatal() 
        {
            //Arrange
            int ScriptId = 1;
            _mockBlocksService.Setup(s => s.GetBlocksByScriptIdAsync(ScriptId)).ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetBlocks(ScriptId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
        }

    }
}

