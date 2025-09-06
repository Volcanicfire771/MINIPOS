using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class PurchaseOrderReceiptService : IPurchaseOrderReceiptService
{
    private readonly POSDbContext _context;

    public PurchaseOrderReceiptService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PurchaseOrderReceipt> GetAll()
        => _context.PurchaseOrderReceipts
                   .Include(d => d.Warehouse)
                   .Include(d => d.PurchaseOrder)
                   .ToList();

    public PurchaseOrderReceipt GetById(int id)
        => _context.PurchaseOrderReceipts.Find(id);

    public void Add(PurchaseOrderReceipt receipt)
    {
        _context.PurchaseOrderReceipts.Add(receipt);
        Save();
    }

    public void Update(PurchaseOrderReceipt receipt)
    {
        _context.PurchaseOrderReceipts.Update(receipt);
        Save();
    }

    public void Delete(int id)
    {
        var detail = _context.PurchaseOrderReceipts.Find(id);
        if (detail != null)
        {
            _context.PurchaseOrderReceipts.Remove(detail);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
