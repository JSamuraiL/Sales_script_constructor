using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace TestControllers.TestBlockConnections;

[TestClass]
public class GetBlockConnection
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
        var id = new int();
        var blockConnection = new BlockConnection { Id = id};
        _mockBlockConnectionsService.Setup(s => s.GetBlockConnectionByIdAsync(id)).ReturnsAsync(blockConnection);

        //Act
        var result = await _controller.GetBlockConnection(id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var objectResult = result as OkObjectResult;
        Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        Assert.AreEqual(blockConnection, objectResult.Value);
    }

    [TestMethod]
    public async Task NotFound()
    {
        //Arrange
        var id = new int();
        var blockConnection = new BlockConnection { Id = id };
        _mockBlockConnectionsService.Setup(s => s.GetBlockConnectionByIdAsync(id)).Throws(new ArgumentNullException());

        //Act
        var result = await _controller.GetBlockConnection(id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
        var objectResult = result as NotFoundObjectResult;
        Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        Assert.AreEqual("Продавца с таким Id не существует", objectResult.Value);
    }

    [TestMethod]
    public async Task Fatal()
    {
        //Arrange
        var id = new int();
        var blockConnection = new BlockConnection { Id = id };
        _mockBlockConnectionsService.Setup(s => s.GetBlockConnectionByIdAsync(id)).Throws(new Exception());

        //Act
        var result = await _controller.GetBlockConnection(id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
