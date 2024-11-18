using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/sellers")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellersService sellersService;
    }
}
