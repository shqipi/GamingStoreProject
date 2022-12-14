using GamingStoreProject.Data;
using GamingStoreProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingStoreProject.Data
{
    public class ApplicationSeeds
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if (!context.Laptops.Any())
                {
                    context.Laptops.AddRange(new List<Laptop>()
                    {
                        new Laptop()
                        {
                           Image="https://health2000canada.com/wp-content/uploads/2020/12/paracetamol.jpg",
                            Description="Joseph Dobson Bonfire / Treacle Mega Lollies.These classic hard boiled lollies are flavoured with treacle and have a dark black appearance.Classic lollies loved by all on bonfire night and around halloween.You get 4 mega lollies for just Â£1 which are individually wrapped",
                            Price=20,
                        }

                    });
                    context.SaveChanges();
                }
            }
        }
    }
}