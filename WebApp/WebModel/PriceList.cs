using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class PriceList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Discount { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
        //public Patient Patient { get; set; } = Patient.Empty;

        [NotMapped]
        public static PriceList Empty { get; private set; } = new PriceList();
    }
}
