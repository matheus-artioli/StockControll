using Microsoft.EntityFrameworkCore;
using StockControlAPI.Models;

namespace StockControlAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.Id_produto);
            modelBuilder.Entity<Fornecedor>().HasKey(s => s.Id_fornecedor);
            modelBuilder.Entity<Movimento>().HasKey(m => m.Id_mov);
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany(s => s.Produtos)
                .HasForeignKey(p => p.Id_fornecedor);
            modelBuilder.Entity<Movimento>()
                .HasOne(m => m.Produto)
                .WithMany()
                .HasForeignKey(m => m.Id_produto);
        }
    }
}