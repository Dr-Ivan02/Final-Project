using Microsoft.EntityFrameworkCore;
using WorkOrderSystem.Models;

namespace WorkOrderSystem.Data
{
    // Database context for the system
    public class AppDbContext : DbContext
    {
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=WorkOrderDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}