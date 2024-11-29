using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;

namespace TestControllers.TestBlockConnections;

[TestClass]
public class CreateBlockConnection
{
    private Mock<ILogger<BlockConnectionsController>> _mockLogger;
    private Mock<IBlockConnectionsService> _mockBlockConnectionsService;
    private BlockConnectionsController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockBlockConnectionsService = new Mock<IBlockConnectionsService>();
        _mockLogger = new Mock<ILogger<BlockConnectionsController>>();
        _controller = new BlockConnectionsController(_mockBlockConnectionsService.Object, _mockLogger.Object);
    }
    [TestMethod]
    public async Task Success()
    {
        //Arrange
        var blockConnection = new BlockConnection {Id = 1};
        _mockBlockConnectionsService.Setup(s => s.AddBlockConnectionAsync(blockConnection)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.CreateBlockConnection(blockConnection);

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
        var blockConnection = new BlockConnection { Id = 1 };
        _mockBlockConnectionsService.Setup(s => s.AddBlockConnectionAsync(blockConnection)).ThrowsAsync(new DbUpdateException());
        _mockBlockConnectionsService.Setup(s => s.BlockConnectionExists(blockConnection.Id)).Returns(true);

        //Act
        var result = await _controller.CreateBlockConnection(blockConnection);

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
        var blockConnection = new BlockConnection { Id = 1 };
        _mockBlockConnectionsService.Setup(s => s.AddBlockConnectionAsync(blockConnection)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.CreateBlockConnection(blockConnection);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
