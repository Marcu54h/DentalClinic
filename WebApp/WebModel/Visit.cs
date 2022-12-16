using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public partial class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }
        public int PatientId { get; set; }
        public int OfficeId { get; set; }

        public virtual Employee Employee { get; set; } = Employee.Empty;

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual Patient Patient { get; set; } = Patient.Empty;
        public virtual Office Office { get; set; } = Office.Empty;

        public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

        public virtual ICollection<Tooth> Teeth { get; set; } = new List<Tooth>();

        [NotMapped]
        public DateTime EndDate => Date.AddHours(1);

        [NotMapped]
        public string Label => "Wizyta";

        [NotMapped]
        public bool filled
        {
            get { return Treatments.Any() | Comments.Any() | Teeth.Any(); }
            set { filled = value; }
        }

        [NotMapped]
        public static Visit Empty { get; private set; } = new Visit();
    }
}
