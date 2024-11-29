using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

namespace TestControllers.TestManagers;

[TestClass]
public class GetBlockConnection
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
        _mockManagersService.Setup(s => s.GetManagerByIdAsync(manager.Id)).ReturnsAsync(manager);

        //Act
        var result = await _controller.GetManager(manager.Id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var objectResult = result as OkObjectResult;
        Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        Assert.AreEqual(manager, objectResult.Value);
    }

    [TestMethod]
    public async Task NotFound()
    {
        //Arrange
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.GetManagerByIdAsync(manager.Id)).Throws(new ArgumentNullException());

        //Act
        var result = await _controller.GetManager(manager.Id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<NotFoundObjectResult>(result);
        var objectResult = result as NotFoundObjectResult;
        Assert.AreEqual(StatusCodes.Status404NotFound, objectResult.StatusCode);
        Assert.AreEqual("Менеджера с таким Id не существует", objectResult.Value);
    }

    [TestMethod]
    public async Task Fatal()
    {
        //Arrange
        var manager = new Manager { Id = Guid.NewGuid(), Name = "string" };
        _mockManagersService.Setup(s => s.GetManagerByIdAsync(manager.Id)).Throws(new Exception());

        //Act
        var result = await _controller.GetManager(manager.Id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
