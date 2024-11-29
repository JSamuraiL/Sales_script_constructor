using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;

namespace TestControllers.TestBlocks;

[TestClass]
public class ChangeBlock
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
        var block = new Block { Id = 1};
        _mockBlocksService.Setup(s => s.UpdateBlockAsync(block)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.ChangeBlock(block);

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
        var block = new Block { Id = 1 };
        _mockBlocksService.Setup(s => s.UpdateBlockAsync(block)).ThrowsAsync(new ArgumentNullException());

        //Act
        var result = await _controller.ChangeBlock(block);

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
        var block = new Block { Id = 1 };
        _mockBlocksService.Setup(s => s.UpdateBlockAsync(block)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.ChangeBlock(block);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
