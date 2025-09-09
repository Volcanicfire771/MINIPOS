using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Controllers
{
    public class VendorsController : Controller
    {
        private readonly IVendorService _service;

        public VendorsController(IVendorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var vendors = _service.GetAll();
            return View(vendors);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _service.Add(vendor);
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        public IActionResult Edit(int id)
        {
            var vendor = _service.GetById(id);
            if (vendor == null) return NotFound();
            return View(vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _service.Update(vendor);
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
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
