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
            _blocksService = blocksService;
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
        }

        [HttpGet("manager/{ScriptId}")]
        public async Task<IEnumerable<Block>> GetBlocks(int ScriptId) 
        {
            return await _blocksService.GetBlocksByScriptIdAsync(ScriptId);
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
            return NoContent();
        }
    }
}
