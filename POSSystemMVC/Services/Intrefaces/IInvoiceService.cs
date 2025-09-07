using POSSystemMVC.Models;
using POSSystemMVC.Models;

namespace POSSystemMVC.Services.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice? GetById(int id);
        void Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(int id);
    }
}
