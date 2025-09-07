using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;



namespace POSSystemMVC.Models
{
    public class SalesOrder
    {
        public int SalesOrderID { get; set; }
        public int CustomerID { get; set; }
        public int BranchID { get; set; }
        
        [Required]
        public DateTime SODate { get; set; }
        [ValidateNever]
        public Branch Branch { get; set; }
        [ValidateNever]
        public Customer Customer { get; set; }


        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();


    }

}