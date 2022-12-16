using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Employee
    {
        public int Id { get; set; }
        public string PWZNumer { get; set; } = string.Empty;
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FavoriteColor { get; set; } = string.Empty;
        public Person Person { get; set; } = Person.Empty;
        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public User User { get; set; } = User.Empty;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [NotMapped]
        public static Employee Empty { get; private set; } = new Employee();
    }
}
