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
            await _dbContext.Database.ExecuteSqlAsync
                ($"INSERT INTO sellers (id, mail, hashed_password, name, surname, patronymic, manager_id) VALUES ({
                    seller.Id},{seller.Mail},{seller.HashedPassword},{seller.Name},{seller.Surname},{seller.Patronymic},{seller.ManagerId})");
        }

        public async Task DeleteSellerAsync(Guid id)
        {
            await _dbContext.Database.ExecuteSqlAsync($"DELETE FROM sellers WHERE id='{id}'");
        }

        public async Task<Seller> GetSellerByIdAsync(Guid id)
        {
            return await _dbContext.Sellers.FromSqlRaw($"SELECT * FROM sellers WHERE id='{id}'").FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Seller>> GetSellersByManagerId(Guid ManagerId)
        {
            return await _dbContext.Sellers.FromSqlRaw($"SELECT * FROM sellers WHERE manager_id='{ManagerId}'").ToListAsync();
        }
        public bool SellerExists(Guid id)
        {
            return _dbContext.Sellers.FromSqlRaw($"SELECT * FROM sellers WHERE id='{id}'").Any();
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            await _dbContext.Database.ExecuteSqlAsync($"UPDATE sellers SET name = {seller.Name}, surname = {seller.Surname},patronymic = {
                seller.Patronymic}, manager_id = {seller.ManagerId}, mail = {seller.Mail}, hashed_password = {seller.HashedPassword}");
        }
    }
}
