using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class WarehouseService : IWarehouseService
{
    private readonly POSDbContext _context;

    public WarehouseService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Warehouse> GetAll()
        => _context.Warehouses
                   .Include(d => d.Branch)
                   .ToList();

    public Warehouse GetById(int id)
        => _context.Warehouses.Find(id);

    public void Add(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        Save();
    }

    public void Update(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
        Save();
    }

    public void Delete(int id)
    {
        var detail = _context.Warehouses.Find(id);
        if (detail != null)
        {
            _context.Warehouses.Remove(detail);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
