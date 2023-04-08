using Fall22Demo.Data;
using Fall22Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fall22Demo.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public ShopController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.OrderBy(c => c.Name);
            return View(categories.ToList());

        }

        public async Task<IActionResult> ByCategory(string category)
        {
            var products = await _context.Products.Where(p => p.Category.Name == category).OrderBy(p => p.Name).ToListAsync();
            ViewData["Category"] = category;
            return View(products);

        }

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("myVar","abc");
            return View();

        }

        public IActionResult GetSession()
        {
            ViewData["myVar"] = HttpContext.Session.GetString("myVar");
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            var product = _context.Products.Find(ProductId);

            var cartItem = new CartItem
            {
                ProductId = ProductId,
                Quantity = Quantity,
                CustomerId = Guid.NewGuid().ToString(),
                Price = product.Price
            };

            _context.Add(cartItem);
            _context.SaveChanges();
            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            HttpContext.Session.SetInt32("OrderTotal", 59);
            var cartItems = _context.CartItems.ToList();
            return View(cartItems);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout([Bind("FirstName, LastName, Address, City, Province, PostalCode, Phone")] Order order) {

            return RedirectToAction("Payment");
        }

        public IActionResult Payment()
        {
            return RedirectToAction("Details", "Orders", new { @id = 1 });
        }

        public IActionResult ShowConfigVar()
        {
            ViewData["ConfigVar"] = _configuration.GetValue<string>("Authentication:Facebook:AppId");
            return View();
        }
    }
}
