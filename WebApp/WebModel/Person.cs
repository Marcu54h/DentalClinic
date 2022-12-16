using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Person
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty; 
        public string PersonalNumber { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        //public Employee Employee { get; set; } = Employee.Empty;
        //public Patient Patient { get; set; } = Patient.Empty;
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        [NotMapped]
        public static Person Empty { get; private set; } = new Person();
    }
}
