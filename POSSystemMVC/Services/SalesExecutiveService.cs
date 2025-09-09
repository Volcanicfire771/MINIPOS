using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;
using Microsoft.EntityFrameworkCore;

namespace POSSystemMVC.Services
{
    public class SalesExecutiveService : ISalesExecutiveService
    {
        private readonly POSDbContext _context;

        public SalesExecutiveService(POSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SalesExecutive> GetAll()
        {
            return _context.SalesExecutives
                .Include(p => p.Branch).ToList();
        }
        public void Update(SalesExecutive ex)
        {
            _context.SalesExecutives.Update(ex);
            _context.SaveChanges();
        }

     



        public void Add(SalesExecutive ex)
        {
            _context.SalesExecutives.Add(ex);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ex = _context.SalesExecutives.Find(id);
            if (ex != null)
            {
                _context.SalesExecutives.Remove(ex);
                _context.SaveChanges();
            }
        }
    }
}
