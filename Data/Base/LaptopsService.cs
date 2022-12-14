using GamingStoreProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStoreProject.Data.Base
{
    public class LaptopsService : EntityBaseRepository<Laptop>, ILaptopsService
    {
        private readonly ApplicationDbContext _context;
        public LaptopsService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Laptop> GetLaptopByIdAsync(int id)
        {
            var laptopDetails = await _context.Laptops.FirstOrDefaultAsync(n => n.Id == id);


            return laptopDetails;
        }
    }
}
