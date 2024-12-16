using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public class EmployeeProject
    {
        [Key]
        public int EmployeeProjectID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [MaxLength(100)]
        public string RoleInProject { get; set; }

        [Required]
        public DateTime AssignedDate { get; set; }

        // Navigation Properties
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
    }
}
