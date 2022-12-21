using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class PriceList : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Discount { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public Patient Patient { get; set; } = Patient.Empty;
        public int PatientId { get; set; }

        [NotMapped]
        public static PriceList Empty { get; private set; } = new PriceList();
    }
}
