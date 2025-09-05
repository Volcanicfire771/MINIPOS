using System.ComponentModel.DataAnnotations;

namespace POSSystemMVC.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }


    }
}
