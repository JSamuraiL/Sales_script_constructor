using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;

namespace SalesScriptConstructor.API.Controllers
{
    [Route("api/blocks")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly IBlocksService _blocksService;
        private readonly ILogger<BlocksController> _logger;

        public BlocksController(IBlocksService blocksService, ILogger<BlocksController> logger) 
        {
            _blocksService = blocksService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Block>> GetBlock(int id) 
        {
            try 
            { 
                return await _blocksService.GetBlockByIdAsync(id);
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Блок с данным id не найден");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpGet("manager/{ScriptId}")]
        public async Task<IEnumerable<Block>> GetBlocks(int ScriptId) 
        {
            try 
            { 
                return await _blocksService.GetBlocksByScriptIdAsync(ScriptId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return (IEnumerable<Block>)StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Block>> CreateBlock (Block block) 
        {
            try
            {
                await _blocksService.AddBlockAsync(block);
            }
            catch(DbUpdateException ex) 
            {
                if (_blocksService.BlockExists(block.Id)) 
                {
                    _logger.LogWarning(ex, "Warning in SomeAction");
                    return BadRequest("Блок с таким id уже существует");
                }
                else throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
            return CreatedAtAction("GetBlock", new { id = block.Id }, block);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Block>> ChangeBlock (Block block) 
        {
            try
            {
                await _blocksService.UpdateBlockAsync(block);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Блок с данным id не найден");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in SomeAction.");
                return StatusCode(500, "Неизвестная ошибка, уже исправляем");
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Block>> DeleteBlock(int id) 
        {
            try 
            {
                await _blocksService.DeleteBlockAsync(id);
            }
            catch (ArgumentNullException ex) 
            {
                _logger.LogWarning(ex, "Warning in SomeAction");
                return NotFound("Блок с данным id не найден");
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
