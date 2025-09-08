using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Intrefaces;

public class PurchaseOrderReceiptService : IPurchaseOrderReceiptService
{
    private readonly POSDbContext _context;

    public PurchaseOrderReceiptService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PurchaseOrderReceipt> GetAll()
        => _context.PurchaseOrderReceipts
                   .Include(r => r.Warehouse)
                   .Include(r => r.Product)
                   .Include(r => r.PurchaseOrder)
                   .ToList();

    public PurchaseOrderReceipt GetById(int id)
        => _context.PurchaseOrderReceipts.Find(id);

    public void Add(PurchaseOrderReceipt receipt)
    {
        _context.PurchaseOrderReceipts.Add(receipt);
        UpdateWarehouseStock(receipt.WarehouseID, receipt.ProductID, receipt.ReceivedQuantity);
        Save();
    }

    public void Update(PurchaseOrderReceipt receipt)
    {
        var existing = _context.PurchaseOrderReceipts
                               .AsNoTracking()
                               .FirstOrDefault(r => r.ReceiptID == receipt.ReceiptID);

        if (existing != null)
        {
            // adjust stock difference
            int diff = receipt.ReceivedQuantity - existing.ReceivedQuantity;
            UpdateWarehouseStock(receipt.WarehouseID, receipt.ProductID, diff);
        }

        _context.PurchaseOrderReceipts.Update(receipt);
        Save();
    }

    public void Delete(int id)
    {
        var receipt = _context.PurchaseOrderReceipts.Find(id);
        if (receipt != null)
        {
            // rollback stock
            UpdateWarehouseStock(receipt.WarehouseID, receipt.ProductID, -receipt.ReceivedQuantity);

            _context.PurchaseOrderReceipts.Remove(receipt);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private void UpdateWarehouseStock(int warehouseId, int productId, int qtyChange)
    {
        var stock = _context.WarehouseStocks
                            .FirstOrDefault(ws => ws.WarehouseID == warehouseId && ws.ProductID == productId);

        if (stock == null)
        {
            // create new row if stock not exists
            stock = new WarehouseStock
            {
                WarehouseID = warehouseId,
                ProductID = productId,
                Quantity = qtyChange
            };
            _context.WarehouseStocks.Add(stock);
        }
        else
        {
            stock.Quantity += qtyChange;
            _context.WarehouseStocks.Update(stock);
        }
    }
}
