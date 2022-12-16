using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Treatment
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? VisitId { get; set; }
        public int? ToothId { get; set; }

        public ICollection<SubTreatment> SubTreatment { get; set; } = new List<SubTreatment>();
        public Visit Visit { get; set; } = Visit.Empty;
        public Tooth Tooth { get; set; } = Tooth.Empty;

        [NotMapped]
        public static Treatment Empty { get; private set; } = new Treatment();
    }
}
