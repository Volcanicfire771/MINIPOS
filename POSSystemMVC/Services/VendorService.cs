using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Services
{
    public class VendorService : IVendorService
    {
        private readonly POSDbContext _context;

        public VendorService(POSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vendor> GetAll() => _context.Vendors.ToList();

        public Vendor? GetById(int id) => _context.Vendors.Find(id);

        public void Add(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
        }

        public void Update(Vendor vendor)
        {
            _context.Vendors.Update(vendor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var vendor = _context.Vendors.Find(id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
                _context.SaveChanges();
            }
        }
    }
}
