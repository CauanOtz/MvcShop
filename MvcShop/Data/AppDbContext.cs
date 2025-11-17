using Microsoft.EntityFrameworkCore;
using MvcShop.Models;

namespace MvcShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // inicializadores para suprimir warnings de propriedades não nulas sem valor no construtor
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    }
}