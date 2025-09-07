using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly POSDbContext _context;

        public InvoiceService(POSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Invoice> GetAll() => _context.Invoices.ToList();

        public Invoice? GetById(int id) => _context.Invoices.Find(id);

        public void Add(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
            }
        }
    }
}
