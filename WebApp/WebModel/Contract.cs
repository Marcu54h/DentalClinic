using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Contract : EntityBase
    {
        public string Type { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string Salary { get; set; } = string.Empty;
        public int? EmployeeId { get; set; }

        [NotMapped]
        public Employee Employee { get; set; } = Employee.Empty;
    }
}
