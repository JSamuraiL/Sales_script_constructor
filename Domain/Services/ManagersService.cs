using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;

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

        public bool ManagerExists(Guid id)
        {
            return _managersRepository.ManagerExists(id);
        }

        public async Task UpdateManagerAsync(Guid id, Manager manager)
        {
            if (id != manager.Id)
            {
                throw new ArgumentException();
            }
            await _managersRepository.UpdateManagerAsync(manager);
        }
    }
}
