using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Sub2Group
    {
        public int Id { get; set; }
        public int? SubGroupId { get; set; }
        public decimal? LowerPrice { get; set; }
        public decimal? UpperPrice { get; set; }
        public string Name { get; set; } = string.Empty;
        public SubGroup SubGroup { get; set; } = SubGroup.Empty;
        [NotMapped]
        public static Sub2Group Empty { get; private set; } = new Sub2Group();
    }
}
