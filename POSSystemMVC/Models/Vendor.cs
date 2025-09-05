namespace POSSystemMVC.Models
{
    public class Vendor
    {
        public int VendorID { get; set; }   // PK
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;


        // Navigation: one vendor has many purchase orders
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    }
}
