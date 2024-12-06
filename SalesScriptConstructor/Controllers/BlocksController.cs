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
        public BlocksController(IBlocksService blocksService) 
        {
            _blocksService = blocksService ?? throw new ArgumentNullException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Block>> GetBlock(int id) 
        {
            try 
            { 
                return await _blocksService.GetBlockByIdAsync(id);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Блок с данным id не найден");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
        }

        [HttpGet("manager/{ScriptId}")]
        public async Task<IEnumerable<Block>> GetBlocks(int ScriptId) 
        {
            try 
            { 
                return await _blocksService.GetBlocksByScriptIdAsync(ScriptId);
            }
            catch (Exception)
            {
                return (IEnumerable<Block>)StatusCode(500, "Неизвестная ошибка");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Block>> CreateBlock (Block block) 
        {
            try
            {
                await _blocksService.AddBlockAsync(block);
            }
            catch(DbUpdateException) 
            {
                if (_blocksService.BlockExists(block.Id)) 
                { 
                    return BadRequest("Блок с таким id уже существует");
                }
                else throw;
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return CreatedAtAction("GetBlock", new { id = block.Id }, block);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Block>> ChangeBlock (Block block, int id) 
        {
            try
            {
                await _blocksService.UpdateBlockAsync(block, id);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Блок с данным id не найден");
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<Block>> DeleteBlock(int id) 
        {
            try 
            {
                await _blocksService.DeleteBlockAsync(id);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Блок с данным id не найден");
            }
            catch (Exception)
            {
                return StatusCode(500, "Неизвестная ошибка");
            }
            return NoContent();
        }
    }
}
