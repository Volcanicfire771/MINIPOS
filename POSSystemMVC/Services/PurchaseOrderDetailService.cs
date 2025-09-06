using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class PurchaseOrderDetailService : IPurchaseOrderDetailService
{
    private readonly POSDbContext _context;

    public PurchaseOrderDetailService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PurchaseOrderDetails> GetAll()
        => _context.PurchaseOrderDetails
                   .Include(d => d.Product)
                   .Include(d => d.PurchaseOrder)
                   .ToList();

    public PurchaseOrderDetails GetById(int id)
        => _context.PurchaseOrderDetails.Find(id);

    public void Add(PurchaseOrderDetails detail)
    {
        _context.PurchaseOrderDetails.Add(detail);
        Save();
    }

    public void Update(PurchaseOrderDetails detail)
    {
        _context.PurchaseOrderDetails.Update(detail);
        Save();
    }

    public void Delete(int id)
    {
        var detail = _context.PurchaseOrderDetails.Find(id);
        if (detail != null)
        {
            _context.PurchaseOrderDetails.Remove(detail);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
