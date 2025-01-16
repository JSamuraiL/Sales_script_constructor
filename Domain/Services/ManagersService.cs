using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;
using SalesScriptConstructor.Domain.Interfaces.ISellers;

namespace SalesScriptConstructor.Domain.Services
{
    public class ManagersService : IManagersService
    {
        private readonly IManagersRepository _managersRepository;
        public ManagersService(IManagersRepository managersRepository) 
        {
            _managersRepository = managersRepository;        
        }

        public async Task AddManagerAsync(Manager manager)
        {
            await _managersRepository.AddManagerAsync(manager);
        }

        public async Task DeleteManagerAsync(Guid id)
        {
            if (!_managersRepository.ManagerExists(id)) throw new ArgumentNullException();
            await _managersRepository.DeleteManagerAsync(id);
        }

        public async Task<Manager> GetManagerByIdAsync(Guid id)
        {
            return await _managersRepository.GetManagerByIdAsync(id)?? throw new ArgumentNullException();
        }

        public async Task<Manager> GetManagerByMailAsync(string mail, string password)
        {
            if (_managersRepository.GetManagerByMailAsync(mail).Result.HashedPassword != password) throw new ArgumentOutOfRangeException();
            return await _managersRepository.GetManagerByMailAsync(mail) ?? throw new ArgumentNullException();
        }

        public bool ManagerExists(Guid id)
        {
            return _managersRepository.ManagerExists(id);
        }

        public async Task UpdateManagerAsync(Manager manager)
        {
            if (!_managersRepository.ManagerExists(manager.Id)) throw new ArgumentNullException();
            await _managersRepository.UpdateManagerAsync(manager);
        }
    }
}
