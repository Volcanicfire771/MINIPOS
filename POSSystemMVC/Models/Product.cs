using System.ComponentModel.DataAnnotations;



namespace POSSystemMVC.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string code { get; set; }
        public decimal CostPrice { get; set; }
        [Required]
        public string? Description { get; set; }
    }

}