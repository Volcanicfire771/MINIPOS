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

    public IActionResult Index(int? purchaseOrderId)
    {
        IEnumerable<PurchaseOrderDetails> details;

        if (purchaseOrderId.HasValue)
        {
            details = _orderDetailService.GetByPurchaseOrderId(purchaseOrderId.Value);

            // Pass selected PO to the ViewBag
            ViewBag.PurchaseOrders = new SelectList(
                _orderService.GetAll(),
                "PurchaseOrderID",
                "PurchaseOrderID",
                purchaseOrderId.Value
            );
        }
        else
        {
            details = _orderDetailService.GetAll();

            // Build dropdown with no preselected value
            ViewBag.PurchaseOrders = new SelectList(
                _orderService.GetAll(),
                "PurchaseOrderID",
                "PurchaseOrderID"
            );
        }

        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");

        return View(details);
    }


    public IActionResult Create(int? id)
    {

        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        if (id.HasValue)
        {
            var details = _orderDetailService.GetByPurchaseOrderId(id);
            return View(details);
        }
        var detaills = _orderDetailService.GetAll();

        return View(detaills);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PurchaseOrderDetails detail)
    {
        if (ModelState.IsValid)
        {
            _orderDetailService.Add(detail);
            return RedirectToAction(nameof(Index), new { purchaseOrderID = detail.PurchaseOrderID });
        }

        // Rebuild dropdowns if validation fails
        ViewBag.PurchaseOrders = new SelectList(_orderService.GetAll(), "PurchaseOrderID", "PurchaseOrderID", detail.PurchaseOrderID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", detail.ProductID);
        return View(detail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int purchaseOrderDetailsID, int quantity)
    {
        _orderDetailService.Update(purchaseOrderDetailsID, quantity);
        return RedirectToAction(nameof(Index));
    }





    public IActionResult Delete(int id)
    {
        try
        {
            _orderDetailService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            TempData["Error"] = "Cannot delete this purchase order because it is used in other records";
            return RedirectToAction(nameof(Index));
        }

    }



}