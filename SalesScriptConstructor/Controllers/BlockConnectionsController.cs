using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/blockConnections")]
    [ApiController]
    public class BlockConnectionsController : ControllerBase
    {
        private readonly ILogger<BlockConnectionsController> _logger;
        private readonly IBlockConnectionsService _blockConnectionsService;
        public BlockConnectionsController(IBlockConnectionsService blockConnectionsService, ILogger<BlockConnectionsController> logger)
        {
            _blockConnectionsService = blockConnectionsService ?? throw new ArgumentNullException();
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlockConnection(int id) 
        {
            try
            {
                var blockConnections = await _blockConnectionsService.GetBlockConnectionByIdAsync(id);
                return Ok(blockConnections);
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Соединения с таким id не существует");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlockConnection (BlockConnection blockConnection) 
        {
            try 
            {
                await _blockConnectionsService.AddBlockConnectionAsync(blockConnection);
            }
            catch (DbUpdateException ex) 
            {
                if (_blockConnectionsService.BlockConnectionExists(blockConnection.Id)) 
                { 
                    _logger.LogWarning(ex, "Warning in SomeAction");
                    return BadRequest("Соединение с таким id уже существует");
                }
                else throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
            return CreatedAtAction("GetBlockConnection", new { id = blockConnection.Id }, blockConnection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlockConnection(int id) 
        {
            try 
            { 
                await _blockConnectionsService.DeleteBlockConnectionAsync(id);
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Соединения с таким id не существует");
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