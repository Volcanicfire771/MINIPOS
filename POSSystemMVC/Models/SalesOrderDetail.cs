using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;



namespace POSSystemMVC.Models
{
    public class SalesOrderDetails
    {
        [Key]
        public int SalesOrderDetailsID { get; set; }
        [Required]
        public int SalesOrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]

        public int Quantity { get; set; } = 0;


        [ValidateNever]
        public SalesOrder SalesOrder { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
        

    }

}