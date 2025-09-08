using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class PurchaseOrderReceiptsController : Controller
{
    private readonly IPurchaseOrderReceiptService _orderReceiptService;
    private readonly IPurchaseOrderService _orderService;
    private readonly IWarehouseService _warehouseService;
    private readonly IProductService _productService;

    public PurchaseOrderReceiptsController(
        IPurchaseOrderReceiptService orderReceiptService,
        IPurchaseOrderService orderService,
        IWarehouseService warehouseService,
        IProductService productService)
    {
        _orderReceiptService = orderReceiptService;
        _orderService = orderService;
        _warehouseService = warehouseService;
        _productService = productService;
    }

    public IActionResult Index(int? WarehouseID, int? PurchaseOrderID)
    {
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID");
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");

        var receipts = _orderReceiptService.GetAll();

        if (WarehouseID.HasValue)
            receipts = receipts.Where(r => r.WarehouseID == WarehouseID.Value).ToList();

        if (PurchaseOrderID.HasValue)
            receipts = receipts.Where(r => r.PurchaseOrderID == PurchaseOrderID.Value).ToList();

        return View(receipts);
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

        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", receipt.PurchaseOrderID);
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name", receipt.WarehouseID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", receipt.ProductID);

        return View("Index", _orderReceiptService.GetAll());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PurchaseOrderReceipt receipt)
    {
        if (ModelState.IsValid)
        {
            _orderReceiptService.Update(receipt);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", receipt.PurchaseOrderID);
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name", receipt.WarehouseID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", receipt.ProductID);

        return View("Index", _orderReceiptService.GetAll());
    }

    public IActionResult Delete(int id)
    {
        _orderReceiptService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
