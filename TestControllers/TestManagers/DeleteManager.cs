using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace TestControllers.TestManagers;

[TestClass]
public class DeleteBlockConnection
{
    private Mock<ILogger<SellersController>> _mockLogger;
    private Mock<ISellersService> _mockSellersService;
    private SellersController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockSellersService = new Mock<ISellersService>();
        _mockLogger = new Mock<ILogger<SellersController>>();
        _controller = new SellersController(_mockSellersService.Object, _mockLogger.Object);
    }

    [TestMethod]
    public async Task Success()
    {
        //Arrange
        var id = Guid.NewGuid();
        _mockSellersService.Setup(s => s.DeleteSellerAsync(id)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.DeleteSeller(id);

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
        var id = Guid.NewGuid();
        _mockSellersService.Setup(s => s.DeleteSellerAsync(id)).ThrowsAsync(new ArgumentNullException());

        //Act
        var result = await _controller.DeleteSeller(id);

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
        var id = Guid.NewGuid();
        _mockSellersService.Setup(s => s.DeleteSellerAsync(id)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.DeleteSeller(id);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
