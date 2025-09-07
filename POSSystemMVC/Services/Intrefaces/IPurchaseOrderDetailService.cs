using POSSystemMVC.Models;

public interface IPurchaseOrderDetailService
{
    IEnumerable<PurchaseOrderDetails> GetAll();
    PurchaseOrderDetails GetById(int id);

    IEnumerable<PurchaseOrderDetails> GetByPurchaseOrderId(int? purchaseOrderId);
    void Add(PurchaseOrderDetails detail);
    void Update(PurchaseOrderDetails detail);
    void Delete(int id);
    void Save();
}
