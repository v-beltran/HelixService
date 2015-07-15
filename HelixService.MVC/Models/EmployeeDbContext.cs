using System.Data.Entity;

namespace HelixService.MVC.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("name=ConnString")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
