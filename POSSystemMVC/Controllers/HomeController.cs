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

        public IActionResult Dashboard()
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
