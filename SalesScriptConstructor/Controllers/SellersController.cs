using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/sellers")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellersService _sellersService;
        public SellersController (ISellersService sellersService)
        {
            _sellersService = sellersService;
        }

        [HttpGet("{ManagerId}")]
        public async Task<IEnumerable<Seller>> GetLinkedSellers(Guid ManagerId) 
        {
            return await _sellersService.GetSellersByManagerId(ManagerId);
        }
    }
}
