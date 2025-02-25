﻿using SalesScriptConstructor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesScriptConstructor.Domain.Interfaces.IManagers
{
    public interface IManagersRepository
    {
        Task<Manager> GetManagerByIdAsync(Guid id);
        Task<Manager> GetManagerByMailAsync(string mail);
        Task AddManagerAsync(Manager manager);
        Task UpdateManagerAsync(Manager manager);
        Task DeleteManagerAsync(Guid id);
        bool ManagerExists(Guid id);
    }
}
