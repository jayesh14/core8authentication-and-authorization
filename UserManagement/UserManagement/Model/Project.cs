using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}
