﻿using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.ISellers
{
    public interface ISellersService
    {
        Task<IEnumerable<Seller>> GetSellersByManagerId(Guid ManagerId);
        Task<Seller> GetSellerByIdAsync(Guid id);
        Task<Seller> GetSellerByMailAsync(string mail, string password);
        Task AddSellerAsync(Seller seller);
        Task UpdateSellerAsync(Seller seller);
        Task DeleteSellerAsync(Guid id);
        bool SellerExists(Guid id);
    }
}
