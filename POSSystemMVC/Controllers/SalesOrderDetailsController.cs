using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

    public IActionResult Index(int salesOrderId)
    {
        if (salesOrderId > 0)
        {
            var details = _orderDetailService.GetBySalesOrderId(salesOrderId);
            return View(details);
        }
        ViewBag.SalesOrders = new SelectList(_orderService.GetAll(), "SalesOrderID", "SalesOrderID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        var detaills = _orderDetailService.GetAll();
        return View(detaills);

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
            return RedirectToAction(nameof(Index));
        }

        // Rebuild dropdowns if validation fails
        ViewBag.SalesOrders = new SelectList(_orderService.GetAll(), "SalesOrderID", "SalesOrderID", detail.SalesOrderID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", detail.ProductID);
        return View(detail);
    }

    [HttpPost]
public IActionResult Update([FromBody] SalesOrderDetails model)
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
