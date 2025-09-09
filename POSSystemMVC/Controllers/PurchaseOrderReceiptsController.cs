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

    public IActionResult Index(int? PurchaseOrderID)
    {
        IEnumerable<PurchaseOrderReceipt> details;
        if (PurchaseOrderID.HasValue)
        {
            details = _orderReceiptService.GetByPurchaseOrderId(PurchaseOrderID.Value);

            ViewBag.PurchaseOrders = new SelectList(
                _orderService.GetAll(),
                "PurchaseOrderID",
                "PurchaseOrderID",
                PurchaseOrderID.Value
            );
        }
        else
        {
            details = _orderReceiptService.GetAll();

            ViewBag.PurchaseOrders = new SelectList(
                _orderService.GetAll(),
                "PurchaseOrderID",
                "PurchaseOrderID"
            );
        }
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name");

        return View(details);
    }

    // CORRECTED Create method - this accepts the form data
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PurchaseOrderReceipt receipt)
    {
        // DEBUG: Check what data we received
        System.Diagnostics.Debug.WriteLine($"Received receipt data:");
        System.Diagnostics.Debug.WriteLine($"PurchaseOrderID: {receipt.PurchaseOrderID}");
        System.Diagnostics.Debug.WriteLine($"WarehouseID: {receipt.WarehouseID}");
        System.Diagnostics.Debug.WriteLine($"ProductID: {receipt.ProductID}");
        System.Diagnostics.Debug.WriteLine($"ReceivedQuantity: {receipt.ReceivedQuantity}");

        // DEBUG: Check ModelState
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
                .ToList();

            System.Diagnostics.Debug.WriteLine("ModelState errors:");
            foreach (var error in errors)
            {
                System.Diagnostics.Debug.WriteLine($"Field: {error.Field}, Errors: {string.Join(", ", error.Errors)}");
            }
        }

        if (ModelState.IsValid)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Calling _orderReceiptService.Add()");
                _orderReceiptService.Add(receipt);
                System.Diagnostics.Debug.WriteLine("Add() completed successfully");
                TempData["SuccessMessage"] = "Receipt created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in Add(): {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                ModelState.AddModelError("", "An error occurred while creating the receipt: " + ex.Message);
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("ModelState is invalid, not calling Add()");
        }

        // Repopulate dropdowns if validation fails
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", receipt.PurchaseOrderID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", receipt.ProductID);
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name", receipt.WarehouseID);
        ViewBag.ShowCreateModal = true;

        return View("Index", _orderReceiptService.GetAll());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PurchaseOrderReceipt receipt)
    {
        if (ModelState.IsValid)
        {
            _orderReceiptService.Update(receipt.ReceiptID); // Pass just the ID as per your interface
            return RedirectToAction(nameof(Index));
        }

        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", receipt.PurchaseOrderID);
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name", receipt.WarehouseID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", receipt.ProductID);

        return View("Index", _orderReceiptService.GetAll());
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _orderService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            TempData["Error"] = "Cannot delete this purchase order because it is used in other records";
            return RedirectToAction(nameof(Index));
        }

    }
}