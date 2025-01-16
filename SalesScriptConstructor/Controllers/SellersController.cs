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
        public async Task<IActionResult> GetSeller(Guid id)
        {
            try 
            {
                var seller = await _sellersService.GetSellerByIdAsync(id); 
                return Ok(seller);
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

        [HttpGet("byMail/{mail}")]
        public async Task<IActionResult> GetSellerByMail(string mail, string password)
        {
            try
            {
                var manager = await _sellersService.GetSellerByMailAsync(mail, password);
                return Ok(manager);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return Conflict("Неверно введен пароль");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Менеджера с такой почтой не существует");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeller(Seller seller) 
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
        public async Task<IActionResult> DeleteSeller(Guid id) 
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

        [HttpPut]
        public async Task<IActionResult> UpdateSeller(Seller seller) 
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
