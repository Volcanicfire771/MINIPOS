using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;



namespace POSSystemMVC.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        [Required]
        public bool Status { get; set; }

        public int SalesOrderID {get; set;}

        [ValidateNever]
        public SalesOrder SalesOrder { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();


    }

}