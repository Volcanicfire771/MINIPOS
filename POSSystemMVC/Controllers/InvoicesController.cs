using Microsoft.AspNetCore.Mvc;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _service;

        public InvoicesController(IInvoiceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var invoice = _service.GetAll();
            return View(invoice);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _service.Add(invoice);
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        public IActionResult Edit(int id)
        {
            var invoice = _service.GetById(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _service.Update(invoice);
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
