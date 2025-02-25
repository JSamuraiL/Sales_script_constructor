﻿using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class ManagersRepository : IManagersRepository
    {
        private readonly PostgreDbContext _dbContext;
        public ManagersRepository(PostgreDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task AddManagerAsync(Manager manager)
        {
            await _dbContext.Managers.AddAsync(manager);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteManagerAsync(Guid id)
        {
            _dbContext.Managers.Remove(await _dbContext.Managers.FindAsync(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Manager> GetManagerByIdAsync(Guid id)
        {
            return await _dbContext.Managers.FindAsync(id);
        }

        public async Task<Manager> GetManagerByMailAsync(string mail)
        {
            return await _dbContext.Managers.Where(Manager => Manager.Mail == mail).FirstOrDefaultAsync();
        }

        public bool ManagerExists(Guid id)
        {
            return _dbContext.Managers.Any(e => e.Id == id);
        }

        public async Task UpdateManagerAsync(Manager manager)
        {
            _dbContext.Entry(manager).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
