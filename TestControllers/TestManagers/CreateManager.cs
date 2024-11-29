using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

namespace TestControllers.TestManagers;

[TestClass]
public class CreateBlockConnection
{
    private Mock<ILogger<ManagersController>> _mockLogger;
    private Mock<IManagersService> _mockManagersService;
    private ManagersController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockManagersService = new Mock<IManagersService>();
        _mockLogger = new Mock<ILogger<ManagersController>>();
        _controller = new ManagersController(_mockManagersService.Object, _mockLogger.Object);
    }

    [TestMethod]
    public async Task Success()
    {
        //Arrange
        var manager = new Manager {Id = Guid.NewGuid(), Name = "string"};
        _mockManagersService.Setup(s => s.AddManagerAsync(manager)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.PostManager(manager);

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
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.AddManagerAsync(manager)).ThrowsAsync(new DbUpdateException());
        _mockManagersService.Setup(s => s.ManagerExists(manager.Id)).Returns(true);

        //Act
        var result = await _controller.PostManager(manager);

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
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.AddManagerAsync(manager)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.PostManager(manager);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("����������� ������, ��� ����������", objectResult.Value);
    }
}