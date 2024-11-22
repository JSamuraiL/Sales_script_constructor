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

        public Task AddBlockAsync(Block block)
        {
            throw new NotImplementedException();
        }

        public bool BlockExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBlockAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Block> GetBlockByIdAsync(int id)
        {
            return await _blocksRepository.GetBlockByIdAsync(id)?? throw new ArgumentNullException();
        }

        public Task<IEnumerable<Block>> GetBlocksByManagerIdAsync(Guid ManagerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBlockAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
