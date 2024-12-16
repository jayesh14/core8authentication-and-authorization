using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserManagement.Model;

namespace UserManagement.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        //public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      //      // Configure composite keys, relationships, and constraints if needed

      //      // Example: Self-referencing relationship for Manager and Subordinates
      //      modelBuilder.Entity<Employee>()
      //          .HasOne(e => e.Manager)
      //          .WithMany(m => m.Subordinates)
      //          .HasForeignKey(e => e.ManagerID)
      //          .OnDelete(DeleteBehavior.Restrict);

      //      // Configure enums or constants for fields like Gender, Status, etc.
      //      // Alternatively, use separate lookup tables.

      //      // Configure default values and other constraints
      //      modelBuilder.Entity<Employee>()
      //          .Property(e => e.Gender)
      //      .HasConversion<string>();

      //      modelBuilder.Entity<Attendance>()
      //          .Property(a => a.Status)
      //          .HasConversion<string>();

      //      modelBuilder.Entity<Leave>()
      //          .Property(l => l.LeaveType)
      //          .HasConversion<string>();

      //      modelBuilder.Entity<Leave>()
      //          .Property(l => l.Status)
      //          .HasConversion<string>();

      //      // Add indexes, unique constraints, etc.
      //      modelBuilder.Entity<Employee>()
      //          .HasIndex(e => e.Email)
      //      .IsUnique();

      //      modelBuilder.Entity<Department>()
      //          .HasIndex(d => d.Name)
      //      .IsUnique();

      //      modelBuilder.Entity<Role>()
      //          .HasIndex(r => r.Title)
      //      .IsUnique();

      //      modelBuilder.Entity<Project>()
      //          .HasIndex(p => p.Name)
      //          .IsUnique();

      //      modelBuilder.Entity<Employee>()
      //.HasMany(e => e.PerformanceReviews)
      //.WithOne(pr => pr.Employee)
      //.HasForeignKey(pr => pr.EmployeeID);

      //      modelBuilder.Entity<PerformanceReview>()
      //  .HasOne(pr => pr.Employee)
      //  .WithMany(e => e.PerformanceReviews)
      //  .HasForeignKey(pr => pr.EmployeeID)
      //  .OnDelete(DeleteBehavior.Cascade);

      //      modelBuilder.Entity<PerformanceReview>()
      //          .HasOne(pr => pr.Reviewer)
      //          .WithMany()
      //          .HasForeignKey(pr => pr.ReviewerID)
      //          .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete

            base.OnModelCreating(modelBuilder);
            //base.OnModelCreating(modelBuilder);
        }
    }
}
