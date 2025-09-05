using POSSystemMVC.Models;

namespace POSSystemMVC.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer? GetById(int id);
        void Add(Customer vendor);
        void Update(Customer vendor);
        void Delete(int id);
    }
}
