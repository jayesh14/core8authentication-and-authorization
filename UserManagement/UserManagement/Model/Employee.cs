using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace UserManagement.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, MaxLength(10)]
        public string Gender { get; set; }

        // Foreign Keys
        public int? DepartmentID { get; set; }
        public int? RoleID { get; set; }
        public int? ManagerID { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        [ForeignKey("ManagerID")]
        public Employee Manager { get; set; }

        public ICollection<Employee> Subordinates { get; set; }
        public ICollection<Salary> Salaries { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public ICollection<Leave> Leaves { get; set; }
    }
}
