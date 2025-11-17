using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcShop.Data;

namespace MvcShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index(int? category)
        {
            if (category.HasValue)
            {
                var cats = await _db.Categories.Include(c => c.Products).Where(c => c.Id == category.Value).ToListAsync();
                return View(cats);
            }
            var categories = await _db.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}