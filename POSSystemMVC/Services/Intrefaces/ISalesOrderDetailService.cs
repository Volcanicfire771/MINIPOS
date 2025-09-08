using POSSystemMVC.Models;

public interface ISalesOrderDetailService
{
    IEnumerable<SalesOrderDetails> GetAll();
    SalesOrderDetails GetById(int id);
    void Add(SalesOrderDetails detail);
    IEnumerable<SalesOrderDetails> GetBySalesOrderId(int salesOrderId);

    void Update(int id, int quantity);
    void Delete(int id);
    void Save();
}
