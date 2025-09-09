using POSSystemMVC.Models;

public interface IInvoiceService
{
    IEnumerable<Invoice> GetAll();
    IEnumerable<Invoice> GetByStatus(bool status);
    Invoice GetById(int id);
    void Add(Invoice invoice);
    void Update(Invoice invoice);
    void Delete(int id);
    void Save();
}