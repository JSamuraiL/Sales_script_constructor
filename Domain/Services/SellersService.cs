using SalesScriptConstructor.Domain.Entities;
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
        public async Task<IEnumerable<Seller>> GetSellersByManagerId(Guid ManagerId)
        {
            return await _sellersRepository.GetSellersByManagerId(ManagerId);
        }
    }
}
