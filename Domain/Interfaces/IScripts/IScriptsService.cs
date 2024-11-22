using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.IScripts
{
    public interface IScriptsService
    {
        Task<Script> GetScriptByIdAsync(int id);
        Task<IEnumerable<Script>> GetScriptsByManagerIdAsync(Guid ManagerId);
        Task AddScriptAsync(Script script);
        Task UpdateScriptAsync(Script script, int id);
        Task DeleteScriptAsync(int id);
        bool ScriptExists(int id);

    }
}
