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
                ModifiedOn = DateTime.Now,

            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                ProductName = "DefaultP2",
                CreatedBy = "Amit",
                CreatedOn = DateTime.Now,
                ModifiedBy = "Amit",
                ModifiedOn = DateTime.Now,

            });
        }
    }
}
