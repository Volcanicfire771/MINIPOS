using POSSystemMVC.Models;

public interface IPurchaseOrderReceiptService
{
    IEnumerable<PurchaseOrderReceipt> GetAll();
    //PurchaseOrderReceipt GetById(int id);
    IEnumerable<PurchaseOrderReceipt> GetByPurchaseOrderId(int? purchaseOrderId);
    void Add(PurchaseOrderReceipt receipt);
    void Update(int id);
    void Delete(int id);
    //void Save();
}
