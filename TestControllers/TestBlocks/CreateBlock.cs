using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;

namespace TestControllers.TestBlocks;

[TestClass]
public class CreateBlock
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
        _mockBlocksService.Setup(s => s.AddBlockAsync(block)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.CreateBlock(block);

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
        var block = new Block { Id = 1 };
        _mockBlocksService.Setup(s => s.AddBlockAsync(block)).ThrowsAsync(new DbUpdateException());
        _mockBlocksService.Setup(s => s.BlockExists(block.Id)).Returns(true);

        //Act
        var result = await _controller.CreateBlock(block);

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
        var block = new Block { Id = 1 };
        _mockBlocksService.Setup(s => s.AddBlockAsync(block)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.CreateBlock(block);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
