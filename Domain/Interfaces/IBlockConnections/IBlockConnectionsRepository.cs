using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.IBlockConnections
{
    public interface IBlockConnectionsRepository
    {
        Task<BlockConnection> GetBlockConnectionByIdAsync(int id);
        Task AddBlockConnectionAsync(BlockConnection blockConnection);
        Task DeleteBlockConnectionAsync(int id);
        bool BlockConnectionExists(int id);
    }
}
