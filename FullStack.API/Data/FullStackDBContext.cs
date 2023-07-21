using FullStack.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace FullStack.API.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext(DbContextOptions options) : base(options) {}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId); // Assuming a Department can have multiple employees, adjust if necessary
        }
    }
}
    