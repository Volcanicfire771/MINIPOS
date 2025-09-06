using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

namespace POSSystemMVC.Services
{
    public class BranchService : IBranchService
    {
        private readonly POSDbContext _context;

        public BranchService(POSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Branch> GetAll() => _context.Branches.ToList();

        public Branch? GetById(int id) => _context.Branches.Find(id);

        public void Add(Branch branch)
        {
            _context.Branches.Add(branch);
            _context.SaveChanges();
        }

        public void Update(Branch branch)
        {
            _context.Branches.Update(branch);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var vendor = _context.Branches.Find(id);
            if (vendor != null)
            {
                _context.Branches.Remove(vendor);
                _context.SaveChanges();
            }
        }
    }
}
