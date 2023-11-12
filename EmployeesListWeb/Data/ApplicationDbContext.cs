using EmployeesListWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeesListWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
