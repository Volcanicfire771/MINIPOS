using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class GraphController : Controller
{
    private readonly POSDbContext _context;

    public GraphController(POSDbContext context)
    {
        _context = context;
    }

    // 1. Monthly Sales Revenue
        public async Task<JsonResult> MonthlySalesRevenue()
        {
            var year = 2024;

            var data = await _context.Invoices
                .Where(i => i.InvoiceDate.Year == year)
                .GroupBy(i => i.InvoiceDate.Month)
                .Select(g => new
                {
                    month = g.Key,
                    totalSales = g.Sum(i => i.Amount)
                })
                .ToListAsync();

            // Ensure all 12 months exist
            var result = Enumerable.Range(1, 12)
                .Select(m => new
                {
                    month = m,
                    totalSales = data.FirstOrDefault(d => d.month == m)?.totalSales ?? 0
                });

            return Json(result);
        }

    // 2. Top Selling Products
    public async Task<JsonResult> TopSellingProducts(int count = 5)
    {
        var data = await _context.InvoiceDetails
            .Include(d => d.Product)
            .GroupBy(d => d.Product.code)
            .Select(g => new
            {
                product = g.Key,
                totalSold = g.Sum(x => x.Quantity),
                revenue = g.Sum(x => x.Quantity * x.Product.CostPrice)
            })
            .OrderByDescending(x => x.totalSold)
            .Take(count)
            .ToListAsync();

        return Json(data);
    }

    // 3. Sales by Category
    public async Task<JsonResult> SalesByCategory()
    {
        var data = await _context.InvoiceDetails
            .Include(d => d.Product)
            .Where(d => d.Product != null)
            .ToListAsync();

        var grouped = data
            .GroupBy(d =>
            {
                var desc = d.Product.Description?.ToLower() ?? "";
                if (desc.Contains("keyboard") || desc.Contains("mouse") || desc.Contains("webcam"))
                    return "Peripherals";
                if (desc.Contains("monitor") || desc.Contains("laptop"))
                    return "Displays";
                if (desc.Contains("chair") || desc.Contains("desk"))
                    return "Furniture";
                if (desc.Contains("headset") || desc.Contains("gaming"))
                    return "Audio/Gaming";
                if (desc.Contains("hard drive") || desc.Contains("usb"))
                    return "Storage";
                return "Other";
            })
            .Select(g => new
            {
                category = g.Key,
                revenue = g.Sum(x => x.Quantity * x.Product.CostPrice)
            })
            .OrderByDescending(x => x.revenue)
            .ToList();

        return Json(grouped);
    }

    // 4. Low Stock Alert
    public async Task<JsonResult> LowStockAlert(int threshold = 10)
    {
        var data = await _context.WarehouseStocks
            .Include(ws => ws.Product)
            .Include(ws => ws.Warehouse)
            .Where(ws => ws.Quantity <= threshold)
            .Select(ws => new
            {
                product = ws.Product.code,
                warehouse = ws.Warehouse.Name,
                currentStock = ws.Quantity,
                description = ws.Product.Description
            })
            .OrderBy(ws => ws.currentStock)
            .ToListAsync();

        return Json(data);
    }

    

}
