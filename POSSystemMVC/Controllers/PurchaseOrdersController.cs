using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;
using POSSystemMVC.Services.Intrefaces;

public class PurchaseOrdersController : Controller
{
    private readonly IPurchaseOrderService _orderService;
    private readonly IVendorService _vendorService;
    private readonly IBranchService _branchService;


    public PurchaseOrdersController(IPurchaseOrderService orderService, IVendorService vendorService, IBranchService branchService)
    {
        _orderService = orderService;
        _vendorService = vendorService;
        _branchService = branchService;
    }

    public IActionResult Index()
    {
        var orders = _orderService.GetAll();
        return View(orders);
    }

    public IActionResult Create()
    {
        ViewBag.Vendors = new SelectList(_vendorService.GetAll(), "VendorID", "Name");
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PurchaseOrder order)
    {
        Console.WriteLine($"VendorID: {order.VendorID}");


        if (ModelState.IsValid)
        {
            _orderService.Add(order);
            return RedirectToAction(nameof(Index));
        }


        ViewBag.Vendors = new SelectList(_vendorService.GetAll(), "VendorID", "Name", order.VendorID);
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name", order.BranchID);

        return View(order);
    }
}
