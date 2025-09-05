using POSSystemMVC.Models;

namespace POSSystemMVC.Services.Interfaces
{
    public interface IVendorService
    {
        IEnumerable<Vendor> GetAll();
        Vendor? GetById(int id);
        void Add(Vendor vendor);
        void Update(Vendor vendor);
        void Delete(int id);
    }
}
