using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcShop.Data;

namespace MvcShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories.Include(c => c.Products).ToListAsync();
            return View(categories);
        }

        public IActionResult Contacts()
        {
            return View();
        }
    }
}