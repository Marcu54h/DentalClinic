using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class SubTreatment : EntityBase
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? TreatmentId { get; set; }

        public virtual Treatment Treatment { get; set; } = Treatment.Empty;
        public virtual ICollection<Sub2Treatment> Sub2Treatment { get; set; } = new List<Sub2Treatment>();

        [NotMapped]
        public static SubTreatment Empty { get; private set; } = new SubTreatment();
    }
}
