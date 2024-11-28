using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SalesScriptConstructor.API.Controllers;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace TestControllers;

[TestClass]
public class GetSeller
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
        var seller = new Seller { Id = Guid.NewGuid(), Name = "string"};
        _mockSellersService.Setup(s => s.GetSellerByIdAsync(id)).ReturnsAsync(seller);

        //Act
        var result = await _controller.GetSeller(id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<OkObjectResult>(result);
        var objectResult = result as OkObjectResult;
        Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
        Assert.AreEqual(seller, objectResult.Value);
    }

    [TestMethod]
    public async Task NotFound()
    {
        //Arrange
        var id = Guid.NewGuid();
        var seller = new Seller { Id = Guid.NewGuid(), Name = "string" };
        _mockSellersService.Setup(s => s.GetSellerByIdAsync(id)).Throws(new ArgumentNullException());

        //Act
        var result = await _controller.GetSeller(id);

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
        var id = Guid.NewGuid();
        var seller = new Seller { Id = Guid.NewGuid(), Name = "string" };
        _mockSellersService.Setup(s => s.GetSellerByIdAsync(id)).Throws(new Exception());

        //Act
        var result = await _controller.GetSeller(id);

        //Accert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<ObjectResult>(result);
        var objectResult = result as ObjectResult;
        Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        Assert.AreEqual("Неизвестная ошибка, уже исправляем", objectResult.Value);
    }
}
