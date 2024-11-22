using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.IBlocks
{
    public interface IBlocksService
    {
        Task<Block> GetBlockByIdAsync(int id);
        Task<IEnumerable<Block>> GetBlocksByManagerIdAsync(Guid ManagerId);
        Task AddBlockAsync(Block block);
        Task UpdateBlockAsync(int id);
        Task DeleteBlockAsync(int id);
        bool BlockExists(int id);
    }
}
