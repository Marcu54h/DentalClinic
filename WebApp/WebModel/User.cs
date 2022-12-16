using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public byte[] Pass { get; set; } = default!;
        public string Role { get; set; } = string.Empty;
        public bool Enabled { get; set; }
        public bool Locked { get; set; }

        public Employee Employee { get; set; } = Employee.Empty;
        public int EmployeeId { get; set; }

        [NotMapped]
        public static User Empty { get; private set; } = new User();
    }
}
