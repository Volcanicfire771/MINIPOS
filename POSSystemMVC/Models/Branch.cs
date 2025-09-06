namespace POSSystemMVC.Models
{
    public class Branch
    {
        public int BranchID { get; set; }   // PK
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        // Navigation: a branch can have many orders, warehouses, etc.
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
        //public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    }
}
