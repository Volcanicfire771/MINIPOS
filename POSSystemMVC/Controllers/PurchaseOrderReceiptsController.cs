using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class PurchaseOrderReceiptsController : Controller
{
    private readonly IPurchaseOrderReceiptService _orderReceiptService;
    private readonly IPurchaseOrderService _orderService;
    private readonly IWarehouseService _warehouseService;

    public PurchaseOrderReceiptsController(
        IPurchaseOrderReceiptService orderReceiptService,
        IPurchaseOrderService orderService,
        IWarehouseService warehouseService)
    {
        _orderReceiptService = orderReceiptService;
        _orderService = orderService;
        _warehouseService = warehouseService;
    }

    public IActionResult Index()
    {
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID");
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        var details = _orderReceiptService.GetAll();
        return View(details);
    }

    public IActionResult Create()
    {
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID");
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PurchaseOrderReceipt receipt)
    {
        if (ModelState.IsValid)
        {
            _orderReceiptService.Add(receipt);   
            return RedirectToAction(nameof(Index));
        }

        // Rebuild dropdowns if validation fails
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", receipt.PurchaseOrderID);
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        return View(receipt);
    }

    [HttpPost]
public IActionResult Update([FromBody] PurchaseOrderReceipt model)
{
    if (ModelState.IsValid)
    {
        _orderReceiptService.Update(model);
        return Ok();
    }

    return BadRequest(ModelState);
}

public IActionResult Delete(int id)
{
    _orderReceiptService.Delete(id);
    return RedirectToAction(nameof(Index));
}



}
