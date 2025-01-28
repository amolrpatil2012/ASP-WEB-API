using ASP.NET_ONETOMANY.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_ONETOMANY.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-Many configuration
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);

        base.OnModelCreating(modelBuilder);
    }
}
