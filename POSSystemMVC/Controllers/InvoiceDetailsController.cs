using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class InvoiceDetailsController : Controller
{
    private readonly IInvoiceDetailService _detailService;
    private readonly IInvoiceService _invoiceService;
    private readonly IProductService _productService;
    private readonly IWarehouseService _warehouseService;


    public InvoiceDetailsController(
        IInvoiceDetailService detailService,
        IInvoiceService invoiceService,
        IProductService productService,
        IWarehouseService warehouseService
        )
    {
        _detailService = detailService;
        _invoiceService = invoiceService;
        _productService = productService;
        _warehouseService = warehouseService;
    }

    public IActionResult Index(int? invoiceId)
    {
        IEnumerable<InvoiceDetail> details;

        if (invoiceId.HasValue)
        {
            details = _detailService.GetByInvoiceId(invoiceId.Value);
        }
        else
        {
            details = _detailService.GetAll();
        }

        ViewBag.InvoiceFilter = invoiceId;
        ViewBag.Invoices = new SelectList(_invoiceService.GetAll(), "InvoiceID", "InvoiceID");
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code");
        ViewBag.Warehouses = new SelectList(
            _warehouseService.GetAll(),
            "WarehouseID",
            "Name"
        );
        return View(details);
    }

    public IActionResult Create(int? invoiceId)
    {
        var model = new InvoiceDetail();

        if (invoiceId.HasValue)
            model.InvoiceID = invoiceId.Value;

        ViewBag.Invoices = new SelectList(
            _invoiceService.GetAll(),
            "InvoiceID",
            "InvoiceID",
            model.InvoiceID // <--- pass the selected value here
        );

        ViewBag.Products = new SelectList(
            _productService.GetAllProducts(),
            "ProductID",
            "code"
        );
        ViewBag.Warehouses = new SelectList(
            _warehouseService.GetAll(),
            "WarehouseID",
            "Name"
        );

        return PartialView("_CreateInvoiceDetailPartial", model);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(InvoiceDetail detail)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _detailService.Add(detail);
                TempData["SuccessMessage"] = "Invoice detail created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Handle stock-related errors
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
            }
        }

        ViewBag.Invoices = new SelectList(_invoiceService.GetAll(), "InvoiceID", "InvoiceID", detail.InvoiceID);
        ViewBag.Products = new SelectList(_productService.GetAllProducts(), "ProductID", "code", detail.ProductID);
        ViewBag.Warehouses = new SelectList(_warehouseService.GetAll(), "WarehouseID", "Name", detail.WarehouseID);

        return PartialView("_CreateInvoiceDetailPartial", detail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(InvoiceDetail detail)
    {
        if (ModelState.IsValid)
        {
            _detailService.Update(detail);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult GetByInvoice(int invoiceId)
    {
        var details = _detailService.GetByInvoiceId(invoiceId);
        return PartialView("_InvoiceDetailsListPartial", details);
    }


    public IActionResult Delete(int id)
    {
        try
        {
            _detailService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            TempData["Error"] = "Cannot delete this purchase order because it is used in other records";
            return RedirectToAction(nameof(Index));
        }

    }
}
