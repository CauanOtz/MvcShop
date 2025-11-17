using MvcShop.Models;

namespace MvcShop.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Categories.Any() || db.Products.Any()) return;

            var cat1 = new Category { Name = "Eletrônicos" };
            var cat2 = new Category { Name = "Livros" };
            var cat3 = new Category { Name = "Roupas" };

            db.Categories.AddRange(cat1, cat2, cat3);
            db.SaveChanges();

            db.Products.AddRange(
                new Product { Name = "Smartphone X", Description = "Um ótimo smartphone", Price = 1999.90m, CategoryId = cat1.Id, ImageUrl = "https://via.placeholder.com/300x200?text=Smartphone" },
                new Product { Name = "Notebook Y", Description = "Notebook potente", Price = 4999.00m, CategoryId = cat1.Id, ImageUrl = "https://via.placeholder.com/300x200?text=Notebook" },
                new Product { Name = "C# Básico", Description = "Livro para iniciantes", Price = 79.90m, CategoryId = cat2.Id, ImageUrl = "https://via.placeholder.com/300x200?text=Livro" },
                new Product { Name = "Camiseta Azul", Description = "Camiseta 100% algodão", Price = 39.90m, CategoryId = cat3.Id, ImageUrl = "https://via.placeholder.com/300x200?text=Camiseta" }
            );

            db.SaveChanges();
        }
    }
}