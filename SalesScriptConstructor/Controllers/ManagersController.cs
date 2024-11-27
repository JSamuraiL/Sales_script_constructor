using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/managers")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagersService _managersService;
        private readonly ILogger<ManagersController> _logger;

        public ManagersController(IManagersService managersService, ILogger<ManagersController> logger)
        {
            _managersService = managersService;
            _logger = logger;
        }

        // GET: api/Managers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(Guid id)
        {
            try
            {
                return await _managersService.GetManagerByIdAsync(id);
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Менеджера с таким Id не существует");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }
        
        // PUT: api/Managers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeManagerDetails(Manager manager)
        {
            try
            {
                await _managersService.UpdateManagerAsync(manager);
            }
            catch (ArgumentNullException ex)
            {
                if (!_managersService.ManagerExists(manager.Id))
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

        // POST: api/Managers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manager>> PostManager(Manager manager)
        {
            try
            {
                await _managersService.AddManagerAsync(manager);
            }
            catch (DbUpdateException ex)
            {
                if (_managersService.ManagerExists(manager.Id))
                {
                    _logger.LogWarning(ex, "Warning in SomeAction");
                    return BadRequest("Менеджер с таким Id уже существует");
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

            return CreatedAtAction("GetManager", new { id = manager.Id }, manager);
        }

        // DELETE: api/Managers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(Guid id)
        {
            try 
            { 
                await _managersService.DeleteManagerAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Менеджера с таким Id не существует");
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
