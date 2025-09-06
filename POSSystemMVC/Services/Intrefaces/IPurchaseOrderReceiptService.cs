using POSSystemMVC.Models;

public interface IPurchaseOrderReceiptService
{
    IEnumerable<PurchaseOrderReceipt> GetAll();
    PurchaseOrderReceipt GetById(int id);
    void Add(PurchaseOrderReceipt receipt);
    void Update(PurchaseOrderReceipt receipt);
    void Delete(int id);
    void Save();
}
