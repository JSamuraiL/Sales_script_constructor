using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class BlocksRepository : IBlocksRepository
    {
        private readonly PostgreDbContext _dbContext;
        public BlocksRepository(PostgreDbContext dbContext) 
        {
            _dbContext = dbContext;
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
            return await _dbContext.Blocks.FindAsync(id);
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
