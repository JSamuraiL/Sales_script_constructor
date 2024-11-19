using Microsoft.EntityFrameworkCore;
using SalesScriptConstructor.Domain.Entities;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Infrastructure.Repositories
{
    public class SellersRepository : ISellersRepository
    {
        private readonly PostgreDbContext _dbContext;
        public SellersRepository(PostgreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Seller>> GetSellersByManagerId(Guid id)
        {
            return await _dbContext.Sellers.Where(Seller => Seller.ManagerId == id).ToListAsync();
        }
    }
}
