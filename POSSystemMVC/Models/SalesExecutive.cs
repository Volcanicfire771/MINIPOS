using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace POSSystemMVC.Models
{
    public class SalesExecutive
    {
        public int SalesExecutiveID { get; set; }   // PK

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public int BranchID { get; set; }
        

        // Navigation: a branch can have many orders, warehouses, etc.
        [ValidateNever]
        public Branch Branch { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    }
}
