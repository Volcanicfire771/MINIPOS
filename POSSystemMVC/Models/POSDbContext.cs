using Microsoft.EntityFrameworkCore;
namespace POSSystemMVC.Models
{
    public class POSDbContext : DbContext
    {
        //define tables in db
        public DbSet<Product> Products { get; set; }


        public POSDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
