using POSSystemMVC.Models;

public interface IInvoiceDetailService
{
    IEnumerable<InvoiceDetail> GetAll();
    IEnumerable<InvoiceDetail> GetByInvoiceId(int? invoiceId);
    InvoiceDetail GetById(int id);
    void Add(InvoiceDetail detail);
    void Update(InvoiceDetail detail);
    void Delete(int id);
    void Save();
}