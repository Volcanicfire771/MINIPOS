using Microsoft.EntityFrameworkCore;
using POSSystemMVC.Models;

public class InvoiceDetailService : IInvoiceDetailService
{
    private readonly POSDbContext _context;

    public InvoiceDetailService(POSDbContext context)
    {
        _context = context;
    }

    public IEnumerable<InvoiceDetail> GetAll()
        => _context.InvoiceDetails
                   .Include(d => d.Invoice)
                   .Include(d => d.Product)
                   .Include(d => d.Warehouse)
                   .ToList();

    public IEnumerable<InvoiceDetail> GetByInvoiceId(int? invoiceId)
        => _context.InvoiceDetails
                   .Include(d => d.Invoice)
                   .Include(d => d.Product)
                   .Include(d => d.Warehouse)
                   .Where(d => d.InvoiceID == invoiceId)
                   .ToList();

    public InvoiceDetail GetById(int id)
        => _context.InvoiceDetails
                   .Include(d => d.Invoice)
                   .Include(d => d.Product)
                   .Include(d => d.Warehouse)
                   .FirstOrDefault(d => d.InvoiceDetailID == id);

    public void Add(InvoiceDetail detail)
    {
        System.Diagnostics.Debug.WriteLine($"=== Adding InvoiceDetail ===");
        System.Diagnostics.Debug.WriteLine($"Product: {detail.ProductID}, Warehouse: {detail.WarehouseID}, Quantity: {detail.Quantity}");

        try
        {
            // 1. Check if enough stock is available
            var stock = _context.WarehouseStocks
                .FirstOrDefault(ws => ws.WarehouseID == detail.WarehouseID
                                   && ws.ProductID == detail.ProductID);

            if (stock == null)
            {
                throw new InvalidOperationException($"No stock found for Product ID {detail.ProductID} in Warehouse ID {detail.WarehouseID}");
            }

            if (stock.Quantity < detail.Quantity)
            {
                throw new InvalidOperationException($"Insufficient stock. Available: {stock.Quantity}, Required: {detail.Quantity}");
            }

            // 2. Add the invoice detail
            _context.InvoiceDetails.Add(detail);
            System.Diagnostics.Debug.WriteLine("InvoiceDetail added to context");

            // 3. Reduce warehouse stock
            stock.Quantity -= detail.Quantity;
            _context.WarehouseStocks.Update(stock);
            System.Diagnostics.Debug.WriteLine($"Stock reduced. New quantity: {stock.Quantity}");

            // 4. Save changes
            _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Changes saved successfully");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Add: {ex.Message}");
            throw;
        }
    }

    public void Update(InvoiceDetail detail)
    {
        System.Diagnostics.Debug.WriteLine($"=== Updating InvoiceDetail ID: {detail.InvoiceDetailID} ===");

        try
        {
            // Find the original detail to calculate stock difference
            var originalDetail = _context.InvoiceDetails
                .AsNoTracking()
                .FirstOrDefault(d => d.InvoiceDetailID == detail.InvoiceDetailID);

            if (originalDetail == null)
            {
                throw new InvalidOperationException($"InvoiceDetail with ID {detail.InvoiceDetailID} not found");
            }

            // Calculate the quantity difference
            var quantityDifference = detail.Quantity - originalDetail.Quantity;
            System.Diagnostics.Debug.WriteLine($"Quantity difference: {quantityDifference}");

            if (quantityDifference != 0)
            {
                // Find the warehouse stock
                var stock = _context.WarehouseStocks
                    .FirstOrDefault(ws => ws.WarehouseID == detail.WarehouseID
                                       && ws.ProductID == detail.ProductID);

                if (stock == null)
                {
                    throw new InvalidOperationException($"No stock found for Product ID {detail.ProductID} in Warehouse ID {detail.WarehouseID}");
                }

                // If quantity increased, we need more stock (reduce available stock)
                // If quantity decreased, we return stock (increase available stock)
                var newStockQuantity = stock.Quantity - quantityDifference;

                if (newStockQuantity < 0)
                {
                    throw new InvalidOperationException($"Insufficient stock for update. Available: {stock.Quantity}, Additional needed: {quantityDifference}");
                }

                stock.Quantity = newStockQuantity;
                _context.WarehouseStocks.Update(stock);
                System.Diagnostics.Debug.WriteLine($"Stock updated. New quantity: {stock.Quantity}");
            }

            // Update the invoice detail
            _context.InvoiceDetails.Update(detail);
            _context.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Update completed successfully");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Update: {ex.Message}");
            throw;
        }
    }

    public void Delete(int id)
    {
        System.Diagnostics.Debug.WriteLine($"=== Deleting InvoiceDetail ID: {id} ===");

        try
        {
            var detail = _context.InvoiceDetails.Find(id);
            if (detail != null)
            {
                // Return stock to warehouse when deleting invoice detail
                var stock = _context.WarehouseStocks
                    .FirstOrDefault(ws => ws.WarehouseID == detail.WarehouseID
                                       && ws.ProductID == detail.ProductID);

                if (stock != null)
                {
                    stock.Quantity += detail.Quantity; // Return the quantity
                    _context.WarehouseStocks.Update(stock);
                    System.Diagnostics.Debug.WriteLine($"Stock returned. New quantity: {stock.Quantity}");
                }
                else
                {
                    // Create new stock record if it doesn't exist
                    var newStock = new WarehouseStock
                    {
                        WarehouseID = detail.WarehouseID,
                        ProductID = detail.ProductID,
                        Quantity = detail.Quantity
                    };
                    _context.WarehouseStocks.Add(newStock);
                    System.Diagnostics.Debug.WriteLine($"New stock record created with quantity: {detail.Quantity}");
                }

                _context.InvoiceDetails.Remove(detail);
                _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine("Delete completed successfully");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Delete: {ex.Message}");
            throw;
        }
    }

    public void Save() => _context.SaveChanges();
}