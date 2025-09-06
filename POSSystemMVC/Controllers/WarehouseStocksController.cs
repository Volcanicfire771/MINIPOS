using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class WarehouseStockController : Controller
{
    private readonly IWarehouseService _warehouseService;
    private readonly IWarehouseStockService _warehouseStockService;
    private readonly IProductService _productService;

    public WarehouseStockController(
        IWarehouseService warehouseService,
        IProductService productService,
        IWarehouseStockService warehouseStockService

        )
    {
        _warehouseStockService = warehouseStockService;
        _warehouseService = warehouseService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "Name");

        var details = _warehouseStockService.GetAll();
        return View(details);
    }

    public IActionResult Create()
    {
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(WarehouseStock stock)
    {
        if (ModelState.IsValid)
        {
            _warehouseStockService.Add(stock);   
            return RedirectToAction(nameof(Index));
        }

        // Rebuild dropdowns if validation fails
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "Name");
        return View(stock);
    }

    [HttpPost]
public IActionResult Update([FromBody] WarehouseStock model)
{
    if (ModelState.IsValid)
    {
        _warehouseStockService.Update(model);
        return Ok();
    }

    return BadRequest(ModelState);
}

public IActionResult Delete(int id)
{
        _warehouseService.Delete(id);
    return RedirectToAction(nameof(Index));
}



}
