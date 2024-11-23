using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Services
{
    public class BlockConnectionsService:IBlockConnectionsService
    {
        private readonly IBlockConnectionsRepository _blockConnectionsRepository;
        public BlockConnectionsService(IBlockConnectionsRepository blockConnectionsRepository)
        {
            _blockConnectionsRepository = blockConnectionsRepository;
        }

        public async Task AddBlockConnectionAsync(BlockConnection blockConnection)
        {
            await _blockConnectionsRepository.AddBlockConnectionAsync(blockConnection);
        }

        public bool BlockConnectionExists(int id)
        {
            return _blockConnectionsRepository.BlockConnectionExists(id);
        }

        public async Task DeleteBlockConnectionAsync(int id)
        {
            if (!_blockConnectionsRepository.BlockConnectionExists(id)) throw new ArgumentNullException();
            await _blockConnectionsRepository.DeleteBlockConnectionAsync(id);
        }

        public async Task<BlockConnection> GetBlockConnectionByIdAsync(int id)
        {
            return await _blockConnectionsRepository.GetBlockConnectionByIdAsync(id)?? throw new ArgumentNullException();
        }
    }
}
