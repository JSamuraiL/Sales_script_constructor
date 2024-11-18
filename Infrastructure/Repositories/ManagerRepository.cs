using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class ManagerRepository : IManagersRepository
    {
        private readonly PostgreDbContext _dbContext;
        public ManagerRepository(PostgreDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task AddManagerAsync(Manager manager)
        {
            _dbContext.Managers.Add(manager);
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
