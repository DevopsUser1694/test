using FunctionAppTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppTest.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                ProductName = "DefaultP1",
                CreatedBy = "Chandan",
                CreatedOn = DateTime.Now,
                ModifiedBy = "Chandan",
                ModifiedOn = DateTime.Now

            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                ProductName = "DefaultP2",
                CreatedBy = "Amit",
                CreatedOn = DateTime.Now,
                ModifiedBy = "Amit",
                ModifiedOn = DateTime.Now

            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 1,
                ItemName = "Item_P1_I1",
                ProductId = 1,
                Count = 1,
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 2,
                ItemName = "Item_P1_I2",
                ProductId = 1,
                Count = 2,
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 3,
                ItemName = "Item_P1_I3",
                ProductId = 1,
                Count = 3,
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 4,
                ItemName = "Item_P2_I1",
                ProductId = 2,
                Count = 1,
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 5,
                ItemName = "Item_P2_I2",
                ProductId = 2,
                Count = 2,
            });
        }
    }
}
