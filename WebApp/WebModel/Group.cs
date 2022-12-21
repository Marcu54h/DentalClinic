using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Group : EntityBase
    { 
        public string Type { get; set; } = string.Empty;
        public int? PriceListId { get; set; }
        public decimal? LowerPrice { get; set; }
        public decimal? UpperPrice { get; set; }

        public PriceList PriceList { get; set; } = PriceList.Empty;
        public ICollection<SubGroup> SubGroups { get; set; } = new List<SubGroup>();

        [NotMapped]
        public static Group Empty { get; private set; } = new Group();
    }
}
