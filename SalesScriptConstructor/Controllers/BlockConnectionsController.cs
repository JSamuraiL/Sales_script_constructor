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
        private readonly IBlockConnectionsService _blockConnectionsService;
        public BlockConnectionsController(IBlockConnectionsService blockConnectionsService)
        {
            _blockConnectionsService = blockConnectionsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlockConnection>> GetBlockConnection(int id) 
        {
            try
            {
                return await _blockConnectionsService.GetBlockConnectionByIdAsync(id);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Соединения с таким id не существует");
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<BlockConnection>> CreateBlockConnection (BlockConnection blockConnection) 
        {
            try 
            {
                await _blockConnectionsService.AddBlockConnectionAsync(blockConnection);
            }
            catch (DbUpdateException) 
            {
                if (_blockConnectionsService.BlockConnectionExists(blockConnection.Id)) 
                    return BadRequest("Соединение с таким id уже существует");
                else throw;
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<BlockConnection>> DeleteBlockConnection(int id) 
        {
            try 
            { 
                await _blockConnectionsService.DeleteBlockConnectionAsync(id);
            }
            catch (ArgumentNullException) 
            {
                return NotFound("Соединения с таким id не существует");
            }
            return NoContent();
        }
    }
}