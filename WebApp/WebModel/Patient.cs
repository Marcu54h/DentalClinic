using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Patient
    {
        public int Id { get; set; }
        public bool? Insured { get; set; }
        public string InsuranceNumber { get; set; } = string.Empty;
        public bool? Disabled { get; set; }
        public string DisabilityType { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? EmployeeId { get; set; }

        public PriceList PriceList { get; set; } = PriceList.Empty;
        public Person Person { get; set; } = Person.Empty;
        public Employee Employee { get; set; } = Employee.Empty;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        [NotMapped]
        public static Patient Empty { get; private set; } = new Patient();
    }
}
