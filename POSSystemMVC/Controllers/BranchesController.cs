using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Controllers
{
    public class BranchesController : Controller
    {
        private readonly IBranchService _service;

        public BranchesController(IBranchService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var branches = _service.GetAll();
            return View(branches);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                _service.Add(branch);
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }


        [HttpPost]
        public IActionResult Update([FromBody] Branch model)
        {
            Console.WriteLine($"Update called: ID={model.BranchID}, Name={model.Name}, Location={model.Location}");

            if (ModelState.IsValid)
            {
                _service.Update(model);  
                return Ok();
            }
            return BadRequest(ModelState);
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
