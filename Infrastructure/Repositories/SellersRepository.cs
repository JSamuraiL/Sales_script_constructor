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

        public async Task AddSellerAsync(Seller seller)
        {
            await _dbContext.Sellers.AddAsync(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSellerAsync(Guid id)
        {
            _dbContext.Sellers.Remove(await _dbContext.Sellers.FindAsync(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Seller> GetSellerByIdAsync(Guid id)
        {
            return await _dbContext.Sellers.FindAsync(id);
        }

        public async Task<Seller> GetSellerByMailAsync(string mail)
        {
            return await _dbContext.Sellers.Where(Seller => Seller.Mail == mail).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Seller>> GetSellersByManagerId(Guid ManagerId)
        {
            return await _dbContext.Sellers.Where(Seller => Seller.ManagerId == ManagerId).ToListAsync();
        }
        public bool SellerExists(Guid id)
        {
            return _dbContext.Sellers.Any(e => e.Id == id);
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            _dbContext.Entry(seller).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
