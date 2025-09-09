using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;
using POSSystemMVC.Services.Intrefaces;

public class SalesExecutivesController : Controller
{
    private readonly ISalesExecutiveService _salesExecutiveService;
    private readonly IBranchService _branchService;

    public SalesExecutivesController(ISalesExecutiveService salesExecutiveService, IBranchService branchService)
    {
        _salesExecutiveService = salesExecutiveService;
        _branchService = branchService;
    }

    public IActionResult Index()
    {
        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
        return View(_salesExecutiveService.GetAll());
    }

    [HttpPost]
    public IActionResult Create(SalesExecutive salesExecutive)
    {
        if (ModelState.IsValid)
        {
            _salesExecutiveService.Add(salesExecutive);
            TempData["Success"] = "Sales Executive created successfully!";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
        return View("Index", _salesExecutiveService.GetAll());
    }

    [HttpPost]
    public IActionResult Edit(SalesExecutive salesExecutive)
    {
        if (ModelState.IsValid)
        {
            _salesExecutiveService.Update(salesExecutive);
            TempData["Success"] = "Sales Executive updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Branches = new SelectList(_branchService.GetAll(), "BranchID", "Name");
        return View("Index", _salesExecutiveService.GetAll());
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _salesExecutiveService.Delete(id);
            TempData["Success"] = "Sales Executive deleted successfully!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Error deleting sales executive: " + ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}