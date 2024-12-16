using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public class Leave
    {
        [Key]
        public int LeaveID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
