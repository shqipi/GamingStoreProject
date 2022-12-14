using GamingStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStoreProject.Data.ViewModels
{
    public class IndexPagesVM
    {
        public IEnumerable<Laptop> Laptops;
        public IEnumerable<Monitor> Monitors;
        public IEnumerable<GraphicCard> GraphicCards;
    }
}
