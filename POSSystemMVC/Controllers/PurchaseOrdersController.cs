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

    public IActionResult Index(int? branchID, int? vendorID)
    {

        var orders = _orderService.Filter();

        if (branchID.HasValue)
        {
            orders = orders.Where(po => po.BranchID == branchID);
        }

        if (vendorID.HasValue)
        {
            orders = orders.Where(po => po.VendorID == vendorID);
        }

        ViewBag.Vendors = new SelectList(_vendorService.GetAll(), "VendorID", "Name");
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
        return View(orders.ToList());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(PurchaseOrder order)
    {
        if (ModelState.IsValid)
        {
            _orderService.Update(order);
            return RedirectToAction(nameof(Index));
        }
        return View(order);
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
