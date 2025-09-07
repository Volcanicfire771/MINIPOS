using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace POSSystemMVC.Models
{
    public class PurchaseOrderDetails
    {
        [Key]
        public int PurchaseOrderDetailsID { get; set; }

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public int ProductID { get; set; }

        public int PurchaseOrderID { get; set; } 

        // Navigation
        [ValidateNever]                            
        public Product? Product { get; set; }

        [ValidateNever]
        public PurchaseOrder? PurchaseOrder { get; set; }
    }
}
