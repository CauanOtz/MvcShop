using Microsoft.AspNetCore.Mvc;
using MvcShop.Data;
using MvcShop.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MvcShop.Extensions;

namespace MvcShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;
        public CartController(AppDbContext db) => _db = db;

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            ViewBag.Total = cart.Sum(i => i.Product.Price * i.Quantity);
            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(int productId, int quantity = 1)
        {
            var product = _db.Products.Find(productId);
            if (product == null) return NotFound();
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null) cart.Add(new CartItem { Product = product, Quantity = quantity });
            else item.Quantity += quantity;
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            TempData["Success"] = "Item adicionado ao carrinho.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int productId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                if (quantity <= 0) cart.Remove(item);
                else item.Quantity = quantity;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
    }
}
