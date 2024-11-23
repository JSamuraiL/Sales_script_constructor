using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("manager/{ManagerId}")]
        public async Task<IEnumerable<Seller>> GetLinkedSellers(Guid ManagerId) 
        {
            try 
            { 
                return await _sellersService.GetSellersByManagerId(ManagerId);
            }
            catch (Exception)
            {
                return (IEnumerable<Seller>)StatusCode(500, "Неизвестная ошибка");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(Guid id)
        {
            try 
            { 
                return await _sellersService.GetSellerByIdAsync(id); 
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Продавца с таким Id не существует");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Seller>> CreateSeller(Seller seller) 
        {
            try
            {
                await _sellersService.AddSellerAsync(seller);
            }
            catch (DbUpdateException) 
            {
                if (_sellersService.SellerExists(seller.Id)) 
                {
                    return BadRequest("Продавец с таким Id уже существует");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return CreatedAtAction("GetSeller", new { id = seller.Id }, seller);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Seller>> DeleteSeller(Guid id) 
        {
            try
            {
                await _sellersService.DeleteSellerAsync(id);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Продавца с таким id не существует");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Seller>> UpdateSeller(Guid id, Seller seller) 
        {
            try
            {
                await _sellersService.UpdateSellerAsync(id, seller);
            }
            catch (ArgumentNullException)
            {
                if (!_sellersService.SellerExists(id))
                {
                    return NotFound("Менеджера с таким Id не существует");
                }
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest("Ваш Id не соответствует Id в запросе");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }

            return NoContent();
        }
    }
}
