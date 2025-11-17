using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcShop.Data;
using MvcShop.Models;

namespace MvcShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db) => _db = db;

        public IActionResult Index()
        {
            var cats = _db.Categories.ToList();
            return View(cats);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Corrija os erros no formulário.";
                return View(category);
            }
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Categoria criada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Corrija os erros no formulário.";
                return View(category);
            }
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Categoria atualizada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var c = await _db.Categories.FindAsync(id);
            if (c != null)
            {
                _db.Categories.Remove(c);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Categoria excluída com sucesso.";
            }
            else
            {
                TempData["Error"] = "Categoria não encontrada.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}