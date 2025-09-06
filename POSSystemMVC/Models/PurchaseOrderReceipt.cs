using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace POSSystemMVC.Models
{
    public class PurchaseOrderReceipt
    {
        [Key]
        public int ReceiptID { get; set; }
        [Required]
        public int PurchaseOrderID { get; set; }

        [Required]
        public int ReceivedQuantity { get; set; } = 0;

        [Required]
        public int WarehouseID { get; set; }

       

        // Navigation
        [ValidateNever]                            
        public Warehouse? Warehouse { get; set; }

        [ValidateNever]
        public PurchaseOrder? PurchaseOrder { get; set; }
    }
}
