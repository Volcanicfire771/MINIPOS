using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;
using Microsoft.EntityFrameworkCore;

namespace POSSystemMVC.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly POSDbContext _context;

        public PurchaseOrderService(POSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.Branch).ToList();
        }

        public PurchaseOrder? GetById(int id)
        {
            return _context.PurchaseOrders
                .Include(p => p.Vendor)
                .Include(p => p.Branch)
                                          .FirstOrDefault(p => p.PurchaseOrderID == id);
        }

        public void Add(PurchaseOrder order)
        {
            _context.PurchaseOrders.Add(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.PurchaseOrders.Find(id);
            if (order != null)
            {
                _context.PurchaseOrders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
