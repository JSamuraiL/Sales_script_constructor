using Microsoft.EntityFrameworkCore;
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
            await _dbContext.Database.ExecuteSqlAsync
                ($"INSERT INTO block_connections (id, previous_block_id, next_block_id) VALUES ({blockConnection.Id}, {
                    blockConnection.PreviousBlockId}, {blockConnection.NextBlockId})");
        }

        public bool BlockConnectionExists(int id)
        {
            return _dbContext.BlockConnections.FromSqlRaw($"SELECT * FROM sellers WHERE id='{id}'").Any();
        }

        public async Task DeleteBlockConnectionAsync(int id)
        {
            await _dbContext.Database.ExecuteSqlAsync($"DELETE FROM block_connections WHERE id='{id}'");
        }

        public async Task<BlockConnection> GetBlockConnectionByIdAsync(int id)
        {
            return await _dbContext.BlockConnections.FromSqlRaw($"SELECT * FROM block_connections WHERE id='{id}'").FirstOrDefaultAsync();
        }
    }
}
