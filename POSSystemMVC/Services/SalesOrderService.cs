using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly POSDbContext _context;

        public SalesOrderService(POSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SalesOrder> GetAll() {
            return _context.SalesOrders
                .Include(p => p.Customer)
                .Include(p => p.Branch).ToList();
        }
        public IEnumerable<SalesOrder> Filter()
        {
            return _context.SalesOrders
                .Include(p => p.Customer)
                .Include(p => p.Branch).AsQueryable();
        }

        public SalesOrder? GetById(int id)
        {
            return _context.SalesOrders
                .Include(p => p.Customer)
                .Include(p => p.Branch)
                                          .FirstOrDefault(p => p.SalesOrderID == id);
        }

        public void Add(SalesOrder so)
        {
            _context.SalesOrders.Add(so);
            _context.SaveChanges();
        }

        public void Update(SalesOrder so)
        {
            _context.SalesOrders.Update(so);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var so = _context.SalesOrders.Find(id);
            if (so != null)
            {
                _context.SalesOrders.Remove(so);
                _context.SaveChanges();
            }
        }
    }
}
