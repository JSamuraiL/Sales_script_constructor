using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return await _blocksService.GetBlockByIdAsync(id);
        }
    }
}
