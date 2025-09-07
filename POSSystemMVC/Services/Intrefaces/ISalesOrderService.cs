using POSSystemMVC.Models;

namespace POSSystemMVC.Services.Interfaces
{
    public interface ISalesOrderService
    {
        IEnumerable<SalesOrder> GetAll();
        SalesOrder? GetById(int id);
        IEnumerable<SalesOrder> Filter();

        void Add(SalesOrder so);
        void Update(SalesOrder so);
        void Delete(int id);
    }
}
