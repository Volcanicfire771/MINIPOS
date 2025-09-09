using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;
using POSSystemMVC.Services.Intrefaces;

namespace POSSystemMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var model = _service.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                _service.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _service.GetById(id);
            if (model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Update([FromBody] Customer model)
        {
            if (model == null || model.CustomerID == 0)
                return BadRequest("Invalid product data");

            var customer = _service.GetById(model.CustomerID);
            if (customer == null)
                return NotFound("Product not found");

            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.Phone = model.Phone ?? "";

            _service.Update(customer);
            return Ok();
        }


        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["Error"] = "Cannot delete this purchase order because it is used in other records";
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
