using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class BlockConnectionsRepository : IBlockConnectionsRepository
    {
        private readonly PostgreDbContext _dbContext;
        public BlockConnectionsRepository (PostgreDbContext dbContext) 
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
        }
        public async Task AddBlockConnectionAsync(BlockConnection blockConnection)
        {
            await _dbContext.BlockConnections.AddAsync(blockConnection);
            await _dbContext.SaveChangesAsync();
        }

        public bool BlockConnectionExists(int id)
        {
            return _dbContext.BlockConnections.Any(c => c.Id == id);
        }

        public async Task DeleteBlockConnectionAsync(int id)
        {
            _dbContext.BlockConnections.Remove(await _dbContext.BlockConnections.FindAsync(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BlockConnection> GetBlockConnectionByIdAsync(int id)
        {
            return await _dbContext.BlockConnections.FindAsync(id);
        }
    }
}
