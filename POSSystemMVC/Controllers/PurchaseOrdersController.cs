using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;
using POSSystemMVC.Services.Intrefaces;

public class PurchaseOrdersController : Controller
{
    private readonly IPurchaseOrderService _orderService;
    private readonly IVendorService _vendorService;

    public PurchaseOrdersController(IPurchaseOrderService orderService, IVendorService vendorService)
    {
        _orderService = orderService;
        _vendorService = vendorService;
    }

    public IActionResult Index()
    {
        var orders = _orderService.GetAll();
        return View(orders);
    }

    public IActionResult Create()
    {
        ViewBag.Vendors = new SelectList(_vendorService.GetAll(), "VendorID", "Name");
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
        return View(order);
    }
}
