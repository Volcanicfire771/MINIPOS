using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace POSSystemMVC.Models
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public int VendorID { get; set; }

        [Required]
        public int BranchID { get; set; }

        // Navigation
        [ValidateNever]                           
        public Vendor? Vendor { get; set; }
        [ValidateNever]
        public Branch? Branch { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public ICollection<PurchaseOrderReceipt> PurchaseOrderReceipts { get; set; } = new List<PurchaseOrderReceipt>();


    }
}
