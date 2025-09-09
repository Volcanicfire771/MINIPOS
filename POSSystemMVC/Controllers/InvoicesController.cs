using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;
using POSSystemMVC.Services.Intrefaces;

public class InvoicesController : Controller
{
    private readonly IInvoiceService _invoiceService;
    private readonly ISalesOrderService _salesOrderService;
    private readonly ISalesExecutiveService _salesExecutiveService;



    public InvoicesController(IInvoiceService invoiceService, ISalesOrderService salesOrderService, IWarehouseService warehouseService, ISalesExecutiveService salesExecutiveService)
    {
        _invoiceService = invoiceService;
        _salesOrderService = salesOrderService;
        _salesExecutiveService = salesExecutiveService;

    }

    public IActionResult Index(bool? status, int? SalesOrderID)
    {
        IEnumerable<Invoice> invoices;
        ViewBag.SalesOrders = new SelectList(_salesOrderService.GetAll(), "SalesOrderID", "SalesOrderID");
        ViewBag.SalesExecutives = new SelectList(_salesExecutiveService.GetAll(), "SalesExecutiveID", "Name");

        // If SalesOrderID is provided in the query string, filter automatically
        if (SalesOrderID.HasValue)
        {
            invoices = _invoiceService.GetBySalesOrderId(SalesOrderID.Value);
            ViewBag.FilteredBySalesOrder = true; // Flag to indicate automatic filtering
            ViewBag.SelectedSalesOrderID = SalesOrderID.Value; // Store the filtered ID

        }
        else if (status.HasValue)
        {
            // Only apply status filter if no SalesOrderID filter is present
            invoices = _invoiceService.GetByStatus(status.Value);
        }
        else
        {
            invoices = _invoiceService.GetAll();
        }

        ViewBag.StatusFilter = status;
        return View(invoices);
    }



    public IActionResult Create()
    {
        ViewBag.SalesOrders = new SelectList(_salesOrderService.GetAll(), "SalesOrderID", "SalesOrderID");
        ViewBag.SalesExecutives = new SelectList(_salesExecutiveService.GetAll(), "SalesExecutiveID", "Name");

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
        ViewBag.SalesExecutives = new SelectList(_salesExecutiveService.GetAll(), "SalesExecutiveID", "Name");

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
        ViewBag.SalesExecutives = new SelectList(_salesExecutiveService.GetAll(), "SalesExecutiveID", "Name");
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
