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
        public DbSet<SalesExecutive> SalesExecutives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Branch ↔ Warehouse (1:N)
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Branch)
                .WithMany(b => b.Warehouses)
                .HasForeignKey(w => w.BranchID)
                .OnDelete(DeleteBehavior.Restrict); // إضافة هذه السطر

            // Branch ↔ PurchaseOrder (1:N)
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(po => po.Branch)
                .WithMany(b => b.PurchaseOrders)
                .HasForeignKey(po => po.BranchID)
                .OnDelete(DeleteBehavior.Restrict); // إضافة هذه السطر

            // Vendor ↔ PurchaseOrder (1:N)
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(po => po.Vendor)
                .WithMany(v => v.PurchaseOrders)
                .HasForeignKey(po => po.VendorID)
                .OnDelete(DeleteBehavior.Restrict); // إضافة هذه السطر

            // PurchaseOrderReceipt → Warehouse
            modelBuilder.Entity<PurchaseOrderReceipt>()
                .HasOne(r => r.Warehouse)
                .WithMany(w => w.PurchaseOrderReceipts)
                .HasForeignKey(r => r.WarehouseID)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseOrderReceipt → PurchaseOrder
            modelBuilder.Entity<PurchaseOrderReceipt>()
                .HasOne(r => r.PurchaseOrder)
                .WithMany(po => po.PurchaseOrderReceipts)
                .HasForeignKey(r => r.PurchaseOrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.SalesExecutive)
                .WithMany(se => se.Invoices)
                .HasForeignKey(i => i.SalesExecutiveID)
                .IsRequired(false) // Make it optional
                .OnDelete(DeleteBehavior.Restrict); // Or Restrict

            modelBuilder.Entity<SalesExecutive>()
                .HasOne(se => se.Branch)
                .WithMany(b => b.SalesExecutives)
                .HasForeignKey(se => se.BranchID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Invoice -> SalesExecutive relationship (make it optional)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.SalesExecutive)
                .WithMany(se => se.Invoices)
                .HasForeignKey(i => i.SalesExecutiveID)
                .IsRequired(false) // This makes the relationship optional
                .OnDelete(DeleteBehavior.Restrict);
        }

        public POSDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
