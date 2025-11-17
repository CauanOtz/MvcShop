using Microsoft.AspNetCore.Mvc;
using MvcShop.Data;

namespace MvcShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        public IActionResult Index()
        {
            return View();
        }
    }
}
