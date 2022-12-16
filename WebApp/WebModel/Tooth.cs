using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Tooth
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int? VisitId { get; set; }

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public Visit Visit { get; set; } = Visit.Empty;

        [NotMapped]
        public static Tooth Empty { get; private set; } = new Tooth();
    }
}
