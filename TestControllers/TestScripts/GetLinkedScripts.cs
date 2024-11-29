using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Interfaces;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using SalesScriptConstructor.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SalesScriptConstructor.Domain.Interfaces.IScripts;

namespace TestControllers.TestScripts
{
    [TestClass]
    public sealed class GetBlocks
    {
        private Mock<ILogger<ScriptsController>> _mockLogger;
        private Mock<IScriptsService> _mockScriptsService;
        private ScriptsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockScriptsService = new Mock<IScriptsService>();
            _mockLogger = new Mock<ILogger<ScriptsController>>();
            _controller = new ScriptsController(_mockScriptsService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Success()
        {
            //Arrange
            var managerId = Guid.NewGuid();
            var scripts = new List<Script> { new Script { Id = 1} };
            _mockScriptsService.Setup(s => s.GetScriptsByManagerIdAsync(managerId)).ReturnsAsync(scripts);

            //Act
            var result = await _controller.GetLinkedScripts(managerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<OkObjectResult>(result);
            var objectResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(scripts,objectResult.Value);
        }

        [TestMethod]
        public async Task Fatal() 
        {
            //Arrange
            var managerId = Guid.NewGuid();
            _mockScriptsService.Setup(s => s.GetScriptsByManagerIdAsync(managerId)).ThrowsAsync(new Exception());

            //Act
            var result = await _controller.GetLinkedScripts(managerId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
        }

    }
}

