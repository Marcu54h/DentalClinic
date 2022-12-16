using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public partial class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public Employee Employee { get; set; } = Employee.Empty;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public Patient Patient { get; set; } = Patient.Empty;
        public Office Office { get; set; } = Office.Empty;

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

        public ICollection<Tooth> Teeth { get; set; } = new List<Tooth>();

        [NotMapped]
        public DateTime EndDate => Date.AddHours(1);

        [NotMapped]
        public string Label => Date.ToString("HH:mm") + " - " + Employee.Person.ToString();

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
