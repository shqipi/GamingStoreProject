using GamingStoreProject.Models;
using GamingStoreProject.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStoreProject.Data.Base
{
    public interface ILaptopsService : IEntityBaseRepository<Laptop>
    {
        Task<Laptop> GetLaptopByIdAsync(int id);
    }
}
