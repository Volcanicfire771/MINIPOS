using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;
using POSSystemMVC.Services.Interfaces;

public class PurchaseOrderReceiptService : IPurchaseOrderReceiptService
{
    private readonly POSDbContext _context;

    public PurchaseOrderReceiptService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PurchaseOrderReceipt> GetAll()
        => _context.PurchaseOrderReceipts
                   .Include(r => r.PurchaseOrder)
                   .Include(r => r.Warehouse)
                   .Include(r => r.Product)
                   .ToList();

    public PurchaseOrderReceipt? GetById(int id)
        => _context.PurchaseOrderReceipts
                   .Include(r => r.PurchaseOrder)
                   .Include(r => r.Warehouse)
                   .Include(r => r.Product)
                   .FirstOrDefault(r => r.ReceiptID == id);

    public void Add(PurchaseOrderReceipt receipt)
    {
        // 1. Add the new receipt record (this creates a NEW ROW every time)
        _context.PurchaseOrderReceipts.Add(receipt);

        // 2. Update warehouse stock (this updates/combines quantities)
        var stock = _context.WarehouseStocks
            .FirstOrDefault(ws => ws.WarehouseID == receipt.WarehouseID
                               && ws.ProductID == receipt.ProductID);

        if (stock != null)
        {
            // Update existing stock
            stock.Quantity += receipt.ReceivedQuantity;
            _context.WarehouseStocks.Update(stock);
        }
        else
        {
            // Create new stock record
            var newStock = new WarehouseStock
            {
                WarehouseID = receipt.WarehouseID,
                ProductID = receipt.ProductID,
                Quantity = receipt.ReceivedQuantity
            };
            _context.WarehouseStocks.Add(newStock);
        }

        // 3. Save all changes
        _context.SaveChanges();
    }

    public void Update(int id)
    {
        var receipt = _context.PurchaseOrderReceipts.Find(id);
        if (receipt != null)
        {
            _context.PurchaseOrderReceipts.Update(receipt);
            _context.SaveChanges();
        }
    }

    public IEnumerable<PurchaseOrderReceipt> GetByPurchaseOrderId(int? purchaseOrderId)
    {
        return _context.PurchaseOrderReceipts
            .Include(d => d.Product)
            .Include(d => d.PurchaseOrder)
            .Include(d => d.Warehouse)
            .Where(d => d.PurchaseOrderID == purchaseOrderId)
            .ToList();
    }

    public void Delete(int id)
    {
        var receipt = _context.PurchaseOrderReceipts.Find(id);
        if (receipt != null)
        {
            // Reduce warehouse stock when deleting a receipt
            var stock = _context.WarehouseStocks
                .FirstOrDefault(ws => ws.WarehouseID == receipt.WarehouseID
                                   && ws.ProductID == receipt.ProductID);

            if (stock != null)
            {
                stock.Quantity -= receipt.ReceivedQuantity;

                // Remove stock record if quantity becomes 0 or negative
                if (stock.Quantity <= 0)
                {
                    _context.WarehouseStocks.Remove(stock);
                }
                else
                {
                    _context.WarehouseStocks.Update(stock);
                }
            }

            _context.PurchaseOrderReceipts.Remove(receipt);
            _context.SaveChanges();
        }
    }
}   