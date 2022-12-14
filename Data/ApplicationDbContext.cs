using GamingStoreProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamingStoreProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product>Products { get; set; }
        public DbSet<PC> PCs { get; set; }
        public DbSet<Laptop> Laptops { get; set; }

        public DbSet<GraphicCard> GraphicCards { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GamingStoreProject.Models.Monitor> Monitor { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<Mouse> Mouses { get; set; }
        public DbSet<MousePad> MousePads { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<Headphone> Headphones { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem>ShoppingCartItems { get; set; }

    }
}
