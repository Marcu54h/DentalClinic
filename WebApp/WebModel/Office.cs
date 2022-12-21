using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Office : EntityBase
    {
        public string Type { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();

        [NotMapped]
        public static Office Empty { get; private set; } = new Office();
    }
}
