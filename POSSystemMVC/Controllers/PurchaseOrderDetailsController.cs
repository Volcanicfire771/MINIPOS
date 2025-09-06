using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class PurchaseOrderDetailsController : Controller
{
    private readonly IPurchaseOrderDetailService _orderDetailService;
    private readonly IPurchaseOrderService _orderService;
    private readonly IProductService _productService;

    public PurchaseOrderDetailsController(
        IPurchaseOrderDetailService orderDetailService,
        IPurchaseOrderService orderService,
        IProductService productService)
    {
        _orderDetailService = orderDetailService;
        _orderService = orderService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        var details = _orderDetailService.GetAll();
        return View(details);
    }

    public IActionResult Create()
    {
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PurchaseOrderDetails detail)
    {
        if (ModelState.IsValid)
        {
            _orderDetailService.Add(detail);   // ✅ fix: use _orderDetailService
            return RedirectToAction(nameof(Index));
        }

        // Rebuild dropdowns if validation fails
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", detail.PurchaseOrderID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", detail.ProductID);
        return View(detail);
    }

    [HttpPost]
public IActionResult Update([FromBody] PurchaseOrderDetails model)
{
    if (ModelState.IsValid)
    {
        _orderDetailService.Update(model);
        return Ok();
    }

    return BadRequest(ModelState);
}

public IActionResult Delete(int id)
{
    _orderDetailService.Delete(id);
    return RedirectToAction(nameof(Index));
}



}
