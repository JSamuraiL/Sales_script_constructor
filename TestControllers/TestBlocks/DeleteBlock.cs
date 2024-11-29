using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace TestControllers.TestBlocks;

[TestClass]
public class DeleteBlock
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
        int id = 1;
        _mockBlocksService.Setup(s => s.DeleteBlockAsync(id)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.DeleteBlock(id);

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
        int id = 1;
        _mockBlocksService.Setup(s => s.DeleteBlockAsync(id)).ThrowsAsync(new ArgumentNullException());

        //Act
        var result = await _controller.DeleteBlock(id);

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
        int id = 1;
        _mockBlocksService.Setup(s => s.DeleteBlockAsync(id)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.DeleteBlock(id);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
