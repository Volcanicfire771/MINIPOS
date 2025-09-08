using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class SalesOrderDetailService : ISalesOrderDetailService
{
    private readonly POSDbContext _context;

    public SalesOrderDetailService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<SalesOrderDetails> GetAll()
        => _context.SalesOrderDetails
                   .Include(d => d.Product)
                   .Include(d => d.SalesOrder)
                       .ThenInclude(o => o.Customer)
                   .Include(d => d.SalesOrder)
                       .ThenInclude(o => o.Branch)
                   .ToList();

    public SalesOrderDetails GetById(int id)
        => _context.SalesOrderDetails
                   .Include(d => d.Product)
                   .Include(d => d.SalesOrder)
                       .ThenInclude(o => o.Customer)
                   .Include(d => d.SalesOrder)
                       .ThenInclude(o => o.Branch)
                   .FirstOrDefault(d => d.SalesOrderDetailsID == id);

    public IEnumerable<SalesOrderDetails> GetBySalesOrderId(int salesOrderId)
    {
        return _context.SalesOrderDetails
            .Include(d => d.Product)
            .Include(d => d.SalesOrder)
                .ThenInclude(o => o.Customer)
            .Include(d => d.SalesOrder)
                .ThenInclude(o => o.Branch)
            .Where(d => d.SalesOrderID == salesOrderId)
            .ToList();
    }

    public void Add(SalesOrderDetails detail)
    {
        _context.SalesOrderDetails.Add(detail);
        Save();
    }

    public void Update(int id, int quantity)
    {
        var detail = _context.SalesOrderDetails.Find(id);
        if (detail != null)
        {
            detail.Quantity = quantity;
            Save();
        }
    }

    public void Delete(int id)
    {
        var detail = _context.SalesOrderDetails.Find(id);
        if (detail != null)
        {
            _context.SalesOrderDetails.Remove(detail);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
