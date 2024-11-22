using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Services
{
    public class ScriptsService : IScriptsService
    {
        private readonly IScriptsRepository _scriptsRepository;
        public ScriptsService(IScriptsRepository scriptsRepository) 
        {
            _scriptsRepository = scriptsRepository;
        }

        public async Task AddScriptAsync(Script script)
        {
            await _scriptsRepository.AddScriptAsync(script);
        }

        public async Task DeleteScriptAsync(int id)
        {
            if (!_scriptsRepository.ScriptExists(id)) throw new ArgumentNullException();
            await _scriptsRepository.DeleteScriptAsync(id);
        }

        public async Task<Script> GetScriptByIdAsync(int id)
        {
            return await _scriptsRepository.GetScriptByIdAsync(id)?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<Script>> GetScriptsByManagerIdAsync(Guid ManagerId)
        {
            return await _scriptsRepository.GetScriptsByManagerIdAsync(ManagerId)?? throw new ArgumentNullException();
        }

        public bool ScriptExists(int id)
        {
            return _scriptsRepository.ScriptExists(id);
        }

        public async Task UpdateScriptAsync(Script script, int id)
        {
            if (!_scriptsRepository.ScriptExists(id)) throw new ArgumentNullException();
            if (script.Id != id) throw new ArgumentOutOfRangeException();
            await _scriptsRepository.UpdateScriptAsync(id);
        }
    }
}
