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
        [ValidateNever]                            // <-- don't validate nav props
        public Vendor? Vendor { get; set; }
        [ValidateNever]
        public Branch? Branch { get; set; }

    }
}
