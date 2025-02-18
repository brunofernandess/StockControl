using Microsoft.EntityFrameworkCore;
using StockControl.Models;

namespace StockControl.Data
{
    public class StockControlDbContext : DbContext
    {
        public StockControlDbContext(DbContextOptions<StockControlDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;


        // método para garantir que o código do produto e a data de validade sejam únicos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.CodigoProduto, p.DataValidade })
                .IsUnique();
        }
        }
    
    }


