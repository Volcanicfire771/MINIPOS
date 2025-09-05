namespace POSSystemMVC.Models
{
    public class Warehouse
    {
        public int WarehouseID { get; set; }   // PK
        public int BranchID { get; set; }      // FK

        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        // Navigation: one warehouse belongs to a branch
        public Branch Branch { get; set; }

        // Navigation: one warehouse has many stock records
        //public ICollection<WarehouseStock> Stocks { get; set; } = new List<WarehouseStock>();
    }
}
