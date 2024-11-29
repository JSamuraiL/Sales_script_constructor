using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace TestControllers.TestManagers;

[TestClass]
public class ChangeBlock
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
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.UpdateManagerAsync(manager)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.ChangeManagerDetails(manager);

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
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.UpdateManagerAsync(manager)).ThrowsAsync(new ArgumentNullException());

        //Act
        var result = await _controller.ChangeManagerDetails(manager);

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
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.UpdateManagerAsync(manager)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.ChangeManagerDetails(manager);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
