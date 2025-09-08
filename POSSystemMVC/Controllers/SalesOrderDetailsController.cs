using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;
using POSSystemMVC.Services.Intrefaces;

public class SalesOrderDetailsController : Controller
{
    private readonly ISalesOrderDetailService _orderDetailService;
    private readonly ISalesOrderService _orderService;
    private readonly IProductService _productService;

    public SalesOrderDetailsController(
        ISalesOrderDetailService orderDetailService,
        ISalesOrderService orderService,
        IProductService productService)
    {
        _orderDetailService = orderDetailService;
        _orderService = orderService;
        _productService = productService;
    }

    public IActionResult Index(int? salesOrderId)
    {
        IEnumerable<SalesOrderDetails> details;

        if (salesOrderId.HasValue && salesOrderId.Value > 0)
        {
            details = _orderDetailService.GetBySalesOrderId(salesOrderId.Value);
        }
        else
        {
            details = _orderDetailService.GetAll();
        }

        ViewBag.SalesOrders = new SelectList(_orderService.GetAll(), "SalesOrderID", "SalesOrderID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");

        return View(details);
    }

    public IActionResult Create()
    {
        ViewBag.SalesOrders = new SelectList(_orderService.GetAll(), "SalesOrderID", "SalesOrderID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(SalesOrderDetails detail)
    {
        if (ModelState.IsValid)
        {
            _orderDetailService.Add(detail);
            return RedirectToAction(nameof(Index), new { salesOrderId = detail.SalesOrderID });
        }

        ViewBag.SalesOrders = new SelectList(_orderService.GetAll(), "SalesOrderID", "SalesOrderID", detail.SalesOrderID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", detail.ProductID);
        return View(detail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int salesOrderDetailsID, int quantity)
    {
        _orderDetailService.Update(salesOrderDetailsID, quantity);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        _orderDetailService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
