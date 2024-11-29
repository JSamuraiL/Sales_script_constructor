using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IScripts;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/scripts")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {
        private readonly IScriptsService _scriptsService;
        private readonly ILogger<ScriptsController> _logger;

        public ScriptsController(IScriptsService scriptsService, ILogger<ScriptsController> logger)
        {
            _scriptsService = scriptsService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScript(int id)
        {
            try
            {
                var script = await _scriptsService.GetScriptByIdAsync(id);
                return Ok(script);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Скрипт с таким Id не существует");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpGet("manager/{ManagerId}")]
        public async Task<IActionResult> GetLinkedScripts(Guid ManagerId) 
        {
            try 
            { 
                var scripts = await _scriptsService.GetScriptsByManagerIdAsync(ManagerId);
                return Ok(scripts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateScript(Script script) 
        {
            try 
            {
                await _scriptsService.AddScriptAsync(script);
            }
            catch (DbUpdateException ex) 
            {
                if (_scriptsService.ScriptExists(script.Id)) 
                {
                    _logger.LogWarning(ex, "Warning in SomeAction");
                    return BadRequest("Скрипт с таким id уже существует");
                }
                else throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
            return CreatedAtAction("GetScript", new { id = script.Id }, script);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScript(int id) 
        {
            try
            {
                await _scriptsService.DeleteScriptAsync(id);
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Скрипта с таким id не существует");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateScript(Script script) 
        {
            try
            {
                await _scriptsService.UpdateScriptAsync(script);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Скрипта с таким id не существует");
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
