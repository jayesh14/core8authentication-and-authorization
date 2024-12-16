using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Employee> Employees { get; set; }
    }
}
