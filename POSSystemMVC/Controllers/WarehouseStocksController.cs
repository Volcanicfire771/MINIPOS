using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class WarehouseStocksController : Controller
{
    private readonly IWarehouseService _warehouseService;
    private readonly IWarehouseStockService _warehouseStockService;
    private readonly IProductService _productService;

    public WarehouseStocksController(
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
        var warehouses = _warehouseService.GetAll();
        var products = _productService.GetAllProducts();

        ViewBag.Warehouses = new SelectList(warehouses, "WarehouseID", "Name");
        ViewBag.Products = new SelectList(products, "ProductID", "code");

        var details = _warehouseStockService.GetAll();
        return View(details);
    }

    public IActionResult Create()
    {
        var warehouses = _warehouseService.GetAll();
        var products = _productService.GetAllProducts();

        ViewBag.Warehouses = new SelectList(warehouses, "WarehouseID", "Name");
        ViewBag.Products = new SelectList(products, "ProductID", "code");

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

        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name", stock.WarehouseID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "Code", stock.ProductID);

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
        _warehouseStockService.Delete(id);
    return RedirectToAction(nameof(Index));
}



}
