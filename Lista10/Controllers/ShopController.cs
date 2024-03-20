using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lista10.Data;
using Lista10.Models;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Lista10.Controllers
{
    [AllowAnonymous]
    
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;

        public ShopController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            
            var articles = await _context.Articles.ToListAsync();
            var categories = await _context.Categories.ToListAsync();

            return View((articles, categories));
        }


        // GET: Shop/Category/5
        public async Task<IActionResult> Category(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            ViewData["ChosenCategory"] = category.CategoryName;


            var articles = await _context.Articles.Where(a => a.CategoryId == id).ToListAsync();
            var categories = await _context.Categories.ToListAsync();


            return View((articles, categories));
        }

        public class ArticleInCart
        {
            public int ArticleId { get; set; }
            public int Amount { get; set; }
            
        }
        
        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int artId)
        {

            List<ArticleInCart> cart = new List<ArticleInCart>();
            
            if (Request.Cookies["cart"] != null)
            {
                
                cart = JsonConvert.DeserializeObject<List<ArticleInCart>>(Request.Cookies["cart"]);
            }

            var item = cart.FirstOrDefault(c => c.ArticleId == artId);

            if (item == null)
            {
                cart.Add(new ArticleInCart { ArticleId = artId, Amount = 1 });
            }
            else
            {
                item.Amount += 1;
            }
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddSeconds(604800);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), option);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("DeleteItem")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int artId)
        {

            List<ArticleInCart> cart = new List<ArticleInCart>();

            if (Request.Cookies["cart"] != null)
            {
                cart = JsonConvert.DeserializeObject<List<ArticleInCart>>(Request.Cookies["cart"]);
            }

            var item = cart.FirstOrDefault(c => c.ArticleId == artId);

            if (item != null)
            {
                cart.Remove(item);
            }
            
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddSeconds(604800);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), option);

            return RedirectToAction(nameof(Cart));
        }
    
        [HttpPost, ActionName("ChangeNumberOfArticles")]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeNumberOfArticles(int artId, int change)
        {

            Console.WriteLine(artId);
            List<ArticleInCart> cart = new List<ArticleInCart>();
            cart = JsonConvert.DeserializeObject<List<ArticleInCart>>(Request.Cookies["cart"]);
            Console.WriteLine(cart);
            var item = cart.FirstOrDefault(c => c.ArticleId == artId);
            Console.WriteLine(item.Amount);
            if (item != null)
            {
                item.Amount += change;
                if (item.Amount == 0)
                {
                    DeleteItem(item.ArticleId);
                    return RedirectToAction(nameof(Cart));

                }
            }
            Console.WriteLine(cart);
            Console.WriteLine(item.Amount);
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddSeconds(604800);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(cart), option);

            return RedirectToAction(nameof(Cart));
        }
        public async Task<IActionResult> Cart()
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

    }
}
