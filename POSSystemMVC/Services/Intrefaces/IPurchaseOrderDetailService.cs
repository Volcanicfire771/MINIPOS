using POSSystemMVC.Models;

public interface IPurchaseOrderDetailService
{
    IEnumerable<PurchaseOrderDetails> GetAll();
    PurchaseOrderDetails GetById(int id);
    void Add(PurchaseOrderDetails detail);
    void Update(PurchaseOrderDetails detail);
    void Delete(int id);
    void Save();
}
