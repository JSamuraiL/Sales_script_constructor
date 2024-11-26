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
        private readonly ILogger<BlockConnectionsController> _logger;

        public ScriptsController(IScriptsService scriptsService, ILogger<BlockConnectionsController> logger)
        {
            _scriptsService = scriptsService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Script>> GetScript(int id)
        {
            try
            {
                return await _scriptsService.GetScriptByIdAsync(id);
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
        public async Task<IEnumerable<Script>> GetLinkedScripts(Guid ManagerId) 
        {
            try { 
                return await _scriptsService.GetScriptsByManagerIdAsync(ManagerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return (IEnumerable<Script>)StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Script>> CreateScript(Script script) 
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
        public async Task<ActionResult<Script>> DeleteScript(int id) 
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

        [HttpPut("{id}")]
        public async Task<ActionResult<Script>> UpdateScript(Script script) 
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
