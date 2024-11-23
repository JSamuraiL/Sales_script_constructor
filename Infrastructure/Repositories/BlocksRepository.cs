using Microsoft.EntityFrameworkCore;
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
        public async Task AddBlockAsync(Block block)
        {
            await _dbContext.Blocks.AddAsync(block);
            await _dbContext.SaveChangesAsync();
        }

        public bool BlockExists(int id)
        {
            return _dbContext.Blocks.Any(b => b.Id == id);
        }

        public async Task DeleteBlockAsync(int id)
        {
            _dbContext.Blocks.Remove(await _dbContext.Blocks.FindAsync(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Block> GetBlockByIdAsync(int id)
        {
            return await _dbContext.Blocks.FindAsync(id);
        }

        public async Task<IEnumerable<Block>> GetBlocksByScriptIdAsync(int ScriptId)
        {
            return await _dbContext.Blocks.Where(Block => Block.ScriptId == ScriptId).ToListAsync();
        }

        public async Task UpdateBlockAsync(Block block)
        {
            _dbContext.Entry(block).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
