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
        private readonly ILogger<SellersController> _logger;

        public SellersController (ISellersService sellersService, ILogger<SellersController> logger)
        {
            _sellersService = sellersService;
            _logger = logger;
        }

        [HttpGet("manager/{ManagerId}")]
        public async Task<IActionResult> GetLinkedSellers(Guid ManagerId)
        {
            try
            {
                var sellers = await _sellersService.GetSellersByManagerId(ManagerId);
                return Ok(sellers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(Guid id)
        {
            try 
            { 
                return await _sellersService.GetSellerByIdAsync(id); 
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Продавца с таким Id не существует");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Seller>> CreateSeller(Seller seller) 
        {
            try
            {
                await _sellersService.AddSellerAsync(seller);
            }
            catch (DbUpdateException ex) 
            {
                if (_sellersService.SellerExists(seller.Id)) 
                {
                    _logger.LogWarning(ex, "Warning in SomeAction");
                    return BadRequest("Продавец с таким Id уже существует");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
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
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Продавца с таким id не существует");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Seller>> UpdateSeller(Seller seller) 
        {
            try
            {
                await _sellersService.UpdateSellerAsync(seller);
            }
            catch (ArgumentNullException ex)
            {
                if (!_sellersService.SellerExists(seller.Id))
                {
                    _logger.LogWarning(ex, "Warning in SomeAction");
                    return NotFound("Менеджера с таким Id не существует");
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }

            return NoContent();
        }
    }
}
