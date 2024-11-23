using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Services
{
    public class BlocksService : IBlocksService
    {
        private readonly IBlocksRepository _blocksRepository;
        public BlocksService(IBlocksRepository blocksRepository)
        {
            _blocksRepository = blocksRepository;
        }

        public async Task AddBlockAsync(Block block)
        {
            await _blocksRepository.AddBlockAsync(block);
        }

        public bool BlockExists(int id)
        {
            return _blocksRepository.BlockExists(id);
        }

        public async Task DeleteBlockAsync(int id)
        {
            if (!_blocksRepository.BlockExists(id)) throw new ArgumentNullException();
            await _blocksRepository.DeleteBlockAsync(id);
        }

        public async Task<Block> GetBlockByIdAsync(int id)
        {
            return await _blocksRepository.GetBlockByIdAsync(id)?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<Block>> GetBlocksByScriptIdAsync(int ScriptId)
        {
            return await _blocksRepository.GetBlocksByScriptIdAsync(ScriptId);
        }

        public async Task UpdateBlockAsync(Block block,int id)
        {
            if (!_blocksRepository.BlockExists(id)) throw new ArgumentNullException();
            if (block.Id != id) throw new ArgumentOutOfRangeException();
            await _blocksRepository.UpdateBlockAsync(block);
        }
    }
}
