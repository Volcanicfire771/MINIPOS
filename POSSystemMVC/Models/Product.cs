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


        public ICollection<WarehouseStock> WarehouseStocks { get; set; } = new List<WarehouseStock>();
        public ICollection<PurchaseOrderDetails> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetails>();

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();



    }

}