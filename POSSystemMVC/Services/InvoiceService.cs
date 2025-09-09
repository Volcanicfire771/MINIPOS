using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class InvoiceService : IInvoiceService
{
    private readonly POSDbContext _context;

    public InvoiceService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Invoice> GetAll()
        => _context.Invoices
                   .Include(i => i.SalesOrder)
                   .Include(i => i.InvoiceDetails)
                   .ToList();

    public IEnumerable<Invoice> GetByStatus(bool status)
        => _context.Invoices
                   .Include(i => i.SalesOrder)
                   .Include(i => i.InvoiceDetails)
                   .Where(i => i.Status == status)
                   .ToList();

    public Invoice GetById(int id)
        => _context.Invoices
                   .Include(i => i.SalesOrder)
                   .Include(i => i.InvoiceDetails)
                   .FirstOrDefault(i => i.InvoiceID == id);

    public void Add(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        Save();
    }

    public void Update(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        Save();
    }
    public IEnumerable<Invoice> GetBySalesOrderId(int? SalesOrderId)
    {
        return _context.Invoices
            .Include(d => d.SalesOrder)
            //.Include(d => d.Warehouse)
            .Where(d => d.SalesOrderID == SalesOrderId)
            .ToList();
    }

    public void Delete(int id)
    {
        var invoice = _context.Invoices.Find(id);
        if (invoice != null)
        {
            _context.Invoices.Remove(invoice);
            Save();
        }
    }

    public void Save() => _context.SaveChanges();
}
