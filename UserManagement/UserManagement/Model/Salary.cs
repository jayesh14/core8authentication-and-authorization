using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Model
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal BaseSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Bonus { get; set; } = 0;

        [Required]
        public DateTime EffectiveDate { get; set; }

        // Navigation Properties
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
