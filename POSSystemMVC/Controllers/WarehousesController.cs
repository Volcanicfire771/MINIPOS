using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

public class WarehousesController : Controller
{
    private readonly IWarehouseService _warehouseService;
    private readonly IBranchService _branchService;

    public WarehousesController(
        IWarehouseService warehouseService,
        IBranchService branchService
        )
    {
        _warehouseService = warehouseService;
        _branchService = branchService;
    }

    public IActionResult Index()
    {
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
        var details = _warehouseService.GetAll();
        return View(details);
    }

    public IActionResult Create()
    {
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Warehouse warehouse)
    {
        if (ModelState.IsValid)
        {
            _warehouseService.Add(warehouse);   
            return RedirectToAction(nameof(Index));
        }

        // Rebuild dropdowns if validation fails
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "BranchID", warehouse.BranchID);
        return View(warehouse);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Warehouse warehouse)
    {
        if (ModelState.IsValid)
        {
            _warehouseService.Update(warehouse);
            return RedirectToAction(nameof(Index));
        }
        return View(warehouse);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _warehouseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            TempData["Error"] = "Cannot delete this purchase order because it is used in other records";
            return RedirectToAction(nameof(Index));
        }

    }



}
