using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace TestControllers;

[TestClass]
public class CreateSeller
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
        var seller = new Seller {Id = Guid.NewGuid(), Name = "string"};
        _mockSellersService.Setup(s => s.AddSellerAsync(seller)).Returns(Task.CompletedTask);

        //Act
        var result = await _controller.CreateSeller(seller);

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
        var seller = new Seller { Id = Guid.NewGuid(), Name = "string" };
        _mockSellersService.Setup(s => s.AddSellerAsync(seller)).ThrowsAsync(new DbUpdateException());
        _mockSellersService.Setup(s => s.SellerExists(seller.Id)).Returns(true);

        //Act
        var result = await _controller.CreateSeller(seller);

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
        var seller = new Seller { Id = Guid.NewGuid(), Name = "string" };
        _mockSellersService.Setup(s => s.AddSellerAsync(seller)).ThrowsAsync(new Exception());

        //Act
        var result = await _controller.CreateSeller(seller);

        //Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
