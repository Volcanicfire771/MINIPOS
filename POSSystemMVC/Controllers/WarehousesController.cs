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
public IActionResult Update([FromBody] Warehouse model)
{
    if (ModelState.IsValid)
    {
        _warehouseService.Update(model);
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
