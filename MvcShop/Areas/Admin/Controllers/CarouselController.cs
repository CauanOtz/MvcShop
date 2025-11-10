using Microsoft.AspNetCore.Mvc;
using MvcShop.Data;
using MvcShop.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace MvcShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CarouselController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            var items = _db.CarouselImages.OrderBy(c => c.SortOrder).ToList();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselImage model, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "carousel");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
                model.ImageUrl = "/uploads/carousel/" + fileName;
            }

            // ensure SortOrder
            if (model.SortOrder == 0)
                model.SortOrder = (_db.CarouselImages.Any() ? _db.CarouselImages.Max(c => c.SortOrder) + 1 : 1);

            _db.CarouselImages.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var item = _db.CarouselImages.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = _db.CarouselImages.Find(id);
            if (item != null)
            {
                // remove file if local
                if (!string.IsNullOrEmpty(item.ImageUrl) && item.ImageUrl.StartsWith("/uploads/"))
                {
                    var filePath = Path.Combine(_env.WebRootPath ?? "wwwroot", item.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }
                _db.CarouselImages.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
