using Application.Domain.AggregatesModels;
using Application.Infrastructure.Data.Context.Maps;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}