using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IScripts;

namespace TestControllers.TestScripts;

[TestClass]
public class CreateBlockConnection
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
        var script = new Script { Id = 1};
        _mockScriptsService.Setup(s => s.AddScriptAsync(script)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.CreateScript(script);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status201Created, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task IsExist()
    {
        //Arrange
        var script = new Script { Id = 1 };
        _mockScriptsService.Setup(s => s.AddScriptAsync(script)).ThrowsAsync(new DbUpdateException());
        _mockScriptsService.Setup(s => s.ScriptExists(script.Id)).Returns(true);

        //Act
        var result = await _controller.CreateScript(script);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        var objectResult = result as BadRequestObjectResult;
        Assert.AreEqual(StatusCodes.Status400BadRequest, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task Fatal()
    {
        //Arrange
        var script = new Script { Id = 1 };
        _mockScriptsService.Setup(s => s.AddScriptAsync(script)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.CreateScript(script);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
