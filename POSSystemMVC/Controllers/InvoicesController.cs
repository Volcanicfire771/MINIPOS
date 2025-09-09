using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

public class InvoicesController : Controller
{
    private readonly IInvoiceService _invoiceService;
    private readonly ISalesOrderService _salesOrderService;

    public InvoicesController(IInvoiceService invoiceService, ISalesOrderService salesOrderService, IWarehouseService warehouseService)
    {
        _invoiceService = invoiceService;
        _salesOrderService = salesOrderService;

    }

    public IActionResult Index(bool? status)
    {
        IEnumerable<Invoice> invoices;
        ViewBag.SalesOrders = new SelectList(_salesOrderService.GetAll(), "SalesOrderID", "SalesOrderID");

        if (status.HasValue)
        {
            invoices = _invoiceService.GetByStatus(status.Value);
        }
        else
        {
            invoices = _invoiceService.GetAll();
        }

        ViewBag.StatusFilter = status; // keep filter value
        return View(invoices);

    }

    public IActionResult Create()
    {
        ViewBag.SalesOrders = new SelectList(_salesOrderService.GetAll(), "SalesOrderID", "SalesOrderID");

        return PartialView("_CreateInvoicePartial", new Invoice());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Invoice invoice)
    {
        if (ModelState.IsValid)
        {
            _invoiceService.Add(invoice);
            return RedirectToAction(nameof(Index));
        }

        // Repopulate dropdown when validation fails
        ViewBag.SalesOrders = new SelectList(_salesOrderService.GetAll(), "SalesOrderID", "SalesOrderID", invoice.SalesOrderID);

        // Reload Index with invoices and re-open modal
        var invoices = _invoiceService.GetAll();
        ViewBag.ShowCreateModal = true;
        return View("Index", invoices);
    }
  
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Invoice invoice)
    {
        if (ModelState.IsValid)
        {
            _invoiceService.Update(invoice);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.SalesOrders = new SelectList(_salesOrderService.GetAll(), "SalesOrderID", "SalesOrderID", invoice.SalesOrderID);
        ViewBag.ShowEditModal = true; // reopen modal if validation fails
        return View("Index", _invoiceService.GetAll());
    }




    public IActionResult Delete(int id)
    {
        try
        {
            _invoiceService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            TempData["Error"] = "Cannot delete this purchase order because it is used in other records";
            return RedirectToAction(nameof(Index));
        }

    }
}
