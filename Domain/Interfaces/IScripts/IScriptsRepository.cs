using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.IScripts
{
    public interface IScriptsRepository
    {
        Task<Script> GetScriptByIdAsync(int id);
        Task<IEnumerable<Script>> GetScriptsByManagerIdAsync(Guid ManagerId);
        Task AddScriptAsync(Script script);
        bool ScriptExists(int id);
    }
}
