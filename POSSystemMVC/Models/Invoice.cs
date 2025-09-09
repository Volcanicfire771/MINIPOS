using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using POSSystemMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace POSSystemMVC.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public int SalesOrderID { get; set; }

        public int? SalesExecutiveID { get; set; }

        [ValidateNever]
        public SalesOrder SalesOrder { get; set; }
        
        [ValidateNever]
        public SalesExecutive SalesExecutive { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public ICollection<SalesExecutive> SalesExecutives { get; set; } = new List<SalesExecutive>();

    }
}

