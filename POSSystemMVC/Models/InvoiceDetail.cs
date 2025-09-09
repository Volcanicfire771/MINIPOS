using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;



namespace POSSystemMVC.Models
{
    public class InvoiceDetail
    {
        public int InvoiceDetailID { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public int WarehouseID { get; set; }

        public int Quantity { get; set; } = 0;

        [ValidateNever]
        public Invoice Invoice { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public Warehouse Warehouse { get; set; }
    }

}