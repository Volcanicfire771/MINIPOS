using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemMVC.Models;
using POSSystemMVC.Services;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Controllers
{
    public class SalesOrdersController : Controller
    {
        private readonly ISalesOrderService _service;
        private readonly ICustomerService _customerService;
        private readonly IBranchService _branchService;
        

        public SalesOrdersController(ISalesOrderService service, ICustomerService customerService, IBranchService branchService)
        {
            _branchService = branchService;
            _service = service;
            _customerService = customerService;
        }

        public IActionResult Index(int? branchID, int? customerID)
        {

            var so = _service.Filter();

            if (branchID.HasValue)
            {
                so = so.Where(po => po.BranchID == branchID);
            }

            if (customerID.HasValue)
            {
                so = so.Where(po => po.CustomerID == customerID);
            }

            ViewBag.Customers = new SelectList(_customerService.GetAll(), "CustomerID", "Name");
            ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
            return View(so.ToList());
        }

        public IActionResult Create() {
            ViewBag.Customers = new SelectList(_customerService.GetAll(), "CustomerID", "Name");
            ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");

            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SalesOrder so)
        {
            if (ModelState.IsValid)
            {
                _service.Add(so);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = new SelectList(_customerService.GetAll(), "CustomerID", "Name");
            ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
            return View(so);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SalesOrder order)
        {
            if (ModelState.IsValid)
            {
                _service.Update(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
