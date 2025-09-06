using POSSystemMVC.Models;

namespace POSSystemMVC.Services.Interfaces
{
    public interface IBranchService
    {
        IEnumerable<Branch> GetAll();
        Branch? GetById(int id);
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(int id);
    }
}
