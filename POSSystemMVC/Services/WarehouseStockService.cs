using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class WarehouseStockService : IWarehouseStockService
{
    private readonly POSDbContext _context;

    public WarehouseStockService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<WarehouseStock> GetAll()
        => _context.WarehouseStocks
                   .Include(d => d.Warehouse)
                   .Include(d => d.Product)
                   .ToList();

    public WarehouseStock GetById(int id)
    {
        return _context.WarehouseStocks.Find(id);
    }

    public void Add(WarehouseStock stock)
    {
        _context.WarehouseStocks.Add(stock);
        Save();
    }

    public void Update(WarehouseStock stock)
    {
        _context.WarehouseStocks.Update(stock);
        Save();
    }

    public void Delete(int id)
    {
        var detail = _context.WarehouseStocks.Find(id);
        if (detail != null)
        {
            _context.WarehouseStocks.Remove(detail);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
