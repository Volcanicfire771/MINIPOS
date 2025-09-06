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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vendor → PurchaseOrders (1:N)
            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(po => po.Vendor)
                .WithMany(v => v.PurchaseOrders)
                .HasForeignKey(po => po.VendorID);

            //// Customer → SalesOrders (1:N)
            //modelBuilder.Entity<SalesOrder>()
            //    .HasOne(so => so.Customer)
            //    .WithMany(c => c.SalesOrders)
            //    .HasForeignKey(so => so.CustomerID);

            // Branch → Warehouses (1:N)
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Branch)
                .WithMany(b => b.Warehouses)
                .HasForeignKey(w => w.BranchID);

            //// Branch → PurchaseOrders (1:N)
            //modelBuilder.Entity<PurchaseOrder>()
            //    .HasOne(po => po.Branch)
            //    .WithMany(b => b.PurchaseOrders)
            //    .HasForeignKey(po => po.BranchID);

            //// Branch → SalesOrders (1:N)
            //modelBuilder.Entity<SalesOrder>()
            //    .HasOne(so => so.Branch)
            //    .WithMany(b => b.SalesOrders)
            //    .HasForeignKey(so => so.BranchID);
        }
        public POSDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
