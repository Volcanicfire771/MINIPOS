using Microsoft.EntityFrameworkCore;
namespace POSSystemMVC.Models
{
    public class POSDbContext : DbContext
    {
        //define tables in db
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<PurchaseOrderReceipt> PurchaseOrderReceipts { get; set; }
        public DbSet<WarehouseStock> WarehouseStocks { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetails> SalesOrderDetails { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Branch ↔ Warehouse (1:N)
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Branch)
                .WithMany(b => b.Warehouses)
                .HasForeignKey(w => w.BranchID);

            // Branch ↔ PurchaseOrder (1:N)
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(po => po.Branch)
                .WithMany(b => b.PurchaseOrders)
                .HasForeignKey(po => po.BranchID);

            // Vendor ↔ PurchaseOrder (1:N)
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(po => po.Vendor)
                .WithMany(v => v.PurchaseOrders)
                .HasForeignKey(po => po.VendorID);

            // PurchaseOrderReceipt → Warehouse
            modelBuilder.Entity<PurchaseOrderReceipt>()
                .HasOne(r => r.Warehouse)
                .WithMany(w => w.PurchaseOrderReceipts)
                .HasForeignKey(r => r.WarehouseID)
                .OnDelete(DeleteBehavior.Restrict); // ❌ no cascade here

            // PurchaseOrderReceipt → PurchaseOrder
            modelBuilder.Entity<PurchaseOrderReceipt>()
                .HasOne(r => r.PurchaseOrder)
                .WithMany(po => po.PurchaseOrderReceipts)
                .HasForeignKey(r => r.PurchaseOrderID)
                .OnDelete(DeleteBehavior.Cascade); // ✅ keep cascade here



        }

        public POSDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
