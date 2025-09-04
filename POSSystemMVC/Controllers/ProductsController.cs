using Microsoft.AspNetCore.Mvc;
using POSSystemMVC.Models;
using POSSystemMVC.Services;

namespace POSSystemMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var products = _service.GetAllProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _service.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _service.GetProductById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //[HttpPost]
        //public IActionResult UpdateProduct([FromBody] Product model)
        //{
        //    if (model == null || model.ProductID == 0)
        //        return BadRequest("Invalid product data");

        //    var existingProduct = _context.Products.Find(model.ProductID);
        //    if (existingProduct == null)
        //        return NotFound("Product not found");

        //    // Update only the fields you're editing
        //    existingProduct.code = model.code;
        //    existingProduct.CostPrice = model.CostPrice;
        //    existingProduct.Description = model.Description ?? "";

        //    _context.SaveChanges();
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult UpdateProduct([FromBody] Product model)
        {
            if (model == null || model.ProductID == 0)
                return BadRequest("Invalid product data");

            var product = _service.GetProductById(model.ProductID);
            if (product == null)
                return NotFound("Product not found");

            product.code = model.code;
            product.CostPrice = model.CostPrice;
            product.Description = model.Description ?? "";

            _service.UpdateProduct(product);
            return Ok();
        }


        public IActionResult Delete(int id)
        {
            _service.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
