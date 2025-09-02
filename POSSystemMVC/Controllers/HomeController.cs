using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using POSSystemMVC.Models;

namespace POSSystemMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly POSDbContext _context;

        public HomeController(POSDbContext context)
        {
            _context = context;
        }
        //[HttpPost]

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult CreateEditProduct(int? id)
        {


            if(id != null) // If an id is received then we are editing
            {
                var ProductInDb = _context.Products.SingleOrDefault(Product => Product.ProductID == id);
                return View(ProductInDb);
            }
            

            return View();
        }

        public IActionResult DeleteProduct(int id)
        {
            var ProductInDb = _context.Products.SingleOrDefault(Product => Product.ProductID == id);
            _context.Products.Remove(ProductInDb);
            _context.SaveChanges();
            return RedirectToAction("DisplayProducts");
            
        }

        public IActionResult CreateEditProductForm(Product model)
        {
           
                _context.Products.Update(model);            
                _context.SaveChanges();
            return RedirectToAction("DisplayProducts");

        }

        public IActionResult TestData()
        {
            var products = _context.Products.ToList();
            return Json(products);
        }

        public IActionResult DisplayProducts()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult UpdateProduct([FromBody] Product model)
        {
            if (model == null || model.ProductID == 0)
                return BadRequest("Invalid product data");

            var existingProduct = _context.Products.Find(model.ProductID);
            if (existingProduct == null)
                return NotFound("Product not found");

            // Update only the fields you're editing
            existingProduct.code = model.code;
            existingProduct.CostPrice = model.CostPrice;
            existingProduct.Description = model.Description ?? "";

            _context.SaveChanges();
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
