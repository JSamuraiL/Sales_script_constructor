﻿using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class ScriptsRepository : IScriptsRepository
    {
        private readonly PostgreDbContext _dbContext;
        public ScriptsRepository(PostgreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task AddScriptAsync(Script script)
        {
            await _dbContext.Scripts.AddAsync(script);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteScriptAsync(int id)
        {
            _dbContext.Scripts.Remove(await _dbContext.Scripts.FindAsync(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Script> GetScriptByIdAsync(int id)
        {
            return await _dbContext.Scripts.FindAsync(id);
        }

        public async Task<IEnumerable<Script>> GetScriptsByManagerIdAsync(Guid ManagerId)
        {
            return await _dbContext.Scripts.Where(Script => Script.CreatorId == ManagerId).ToListAsync();
        }

        public bool ScriptExists(int id)
        {
            return _dbContext.Scripts.Any(e => e.Id == id);
        }

        public async Task UpdateScriptAsync(Script script)
        {
            _dbContext.Entry(script).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
