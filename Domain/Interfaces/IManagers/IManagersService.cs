using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.IManagers
{
    public interface IManagersService
    {
        Task<Manager> GetManagerByIdAsync(Guid id);
        Task AddManagerAsync(Manager manager);
        Task UpdateManagerAsync(Manager manager);
        Task DeleteManagerAsync(Guid id);
        bool ManagerExists(Guid id);
    }
}
