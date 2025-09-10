using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POSSystemMVC.Models;
using System.Diagnostics;
using System.Linq;

namespace POSSystemMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly POSDbContext _context;

        public HomeController(POSDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var salesData = _context.Invoices
                .GroupBy(i => i.SalesExecutive.Branch)
                .Select(g => new
                {
                    BranchId = g.Key.BranchID,
                    BranchName = g.Key.Name,
                    Total = g.Sum(i => i.InvoiceDetails.Sum(d => d.Quantity * d.Product.CostPrice))
                })
                .ToList();


            ViewBag.BranchIds = JsonConvert.SerializeObject(salesData.Select(d => d.BranchId));
            ViewBag.BranchLabels = JsonConvert.SerializeObject(salesData.Select(d => d.BranchName));
            ViewBag.BranchTotals = JsonConvert.SerializeObject(salesData.Select(d => d.Total));

            return View();
        }

        public IActionResult SalesByExecutive(int branchId)
        {
            var data = _context.Invoices
                .Where(i => i.SalesExecutive.BranchID == branchId)
                .GroupBy(i => i.SalesExecutive.Name)
                .Select(g => new
                {
                    Executive = g.Key,
                    Total = g.Sum(i => i.InvoiceDetails.Sum(d => d.Quantity * d.Product.CostPrice))
                })
                .ToList();

            return Json(data);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
