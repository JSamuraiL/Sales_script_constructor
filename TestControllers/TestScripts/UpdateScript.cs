using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IScripts;

namespace TestControllers.TestScripts;

[TestClass]
public class ChangeBlock
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
        var script = new Script { Id = 1 };
        _mockScriptsService.Setup(s => s.UpdateScriptAsync(script)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.UpdateScript(script);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<NoContentResult>(result);
        var objectResult = result as NoContentResult;
        Assert.AreEqual(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task NotFound()
    {
        //Arrange
        var script = new Script { Id = 1 };
        _mockScriptsService.Setup(s => s.UpdateScriptAsync(script)).ThrowsAsync(new ArgumentNullException());

        //Act
        var result = await _controller.UpdateScript(script);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
        var objectResult = result as NotFoundObjectResult;
        Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task Fatal()
    {
        //Arrange
        var script = new Script { Id = 1 };
        _mockScriptsService.Setup(s => s.UpdateScriptAsync(script)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.UpdateScript(script);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
