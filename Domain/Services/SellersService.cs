using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.IManagers;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Services
{
    public class SellersService : ISellersService
    {
        private readonly ISellersRepository _sellersRepository;
        public SellersService(ISellersRepository sellersRepository) 
        {
            _sellersRepository = sellersRepository;
        }

        public async Task AddSellerAsync(Seller seller)
        {
            await _sellersRepository.AddSellerAsync(seller);
        }

        public async Task DeleteSellerAsync(Guid id)
        {
            if (!_sellersRepository.SellerExists(id)) throw new ArgumentNullException();
            await _sellersRepository.DeleteSellerAsync(id);
        }

        public async Task<Seller> GetSellerByIdAsync(Guid id)
        {
            return await _sellersRepository.GetSellerByIdAsync(id)?? throw new ArgumentNullException();
        }

        public async Task<Seller> GetSellerByMailAsync(string mail, string password)
        {
            if (_sellersRepository.GetSellerByMailAsync(mail).Result.HashedPassword != password) throw new ArgumentOutOfRangeException();
            return await _sellersRepository.GetSellerByMailAsync(mail) ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<Seller>> GetSellersByManagerId(Guid ManagerId)
        {
            return await _sellersRepository.GetSellersByManagerId(ManagerId);
        }

        public bool SellerExists(Guid id)
        {
            return _sellersRepository.SellerExists(id);
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            if (!_sellersRepository.SellerExists(seller.Id)) throw new ArgumentNullException();
            await _sellersRepository.UpdateSellerAsync(seller);
        }
    }
}
