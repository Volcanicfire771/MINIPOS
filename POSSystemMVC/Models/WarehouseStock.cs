using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace POSSystemMVC.Models
{
    public class WarehouseStock
    {
        public int WarehouseStockID { get; set; }   // PK
        public int WarehouseID { get; set; }      // FK

        public int ProductID { get; set; }
        public int Quantity { get; set; } = 0;

        [ValidateNever]                            
        public Warehouse Warehouse { get; set; }
        [ValidateNever]
        public Product Product { get; set; }

    }
}
