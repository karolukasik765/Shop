using Azure.Core;
using Azure;
using Lista10.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lista10.Data;
using Newtonsoft.Json;
using static Lista10.Controllers.ShopController;
using Microsoft.AspNetCore.Authorization;

namespace Lista10.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private readonly ShopDbContext _context;

        public OrdersController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
             if (_context.Articles == null || _context.Categories == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles.ToListAsync();
            var categories = await _context.Categories.ToListAsync();


            List<ArticleInCart> cart = new List<ArticleInCart>();
            if (Request.Cookies["cart"] != null)
            {
                cart = JsonConvert.DeserializeObject<List<ArticleInCart>>(Request.Cookies["cart"]);
            }

            List<(Article, int)> cartArticles = articles.Join(cart, a => a.ArticleId, c => c.ArticleId, (a, c) => (a, c.Amount)).ToList();

            return View((cartArticles, categories));
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder([Bind("Name,LastName,TelephoneNumber,Street,City,PostalCode,PaymentMethod")] Order order)
        {
            if (Request.Cookies.ContainsKey("cart"))
            {
                Response.Cookies.Delete("cart");
            }
            return View(order);
        }
    }
}
