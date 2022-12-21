using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Comment : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int? VisitId { get; set; }
        public int? PatientId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ToothId { get; set; }

        public Visit Visit { get; set; } = Visit.Empty;
        public Patient Patient { get; set; } = Patient.Empty;
        public Employee Employee { get; set; } = Employee.Empty;
        public Tooth Tooth { get; set; } = Tooth.Empty;

        [NotMapped]
        public static Comment Empty { get; private set; } = new Comment();
    }
}
