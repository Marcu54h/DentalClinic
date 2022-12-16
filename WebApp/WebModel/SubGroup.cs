using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class SubGroup
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? LowerPrice { get; set; }
        public decimal? UpperPrice { get; set; }

        public Group Group { get; set; } = Group.Empty;
        public ICollection<Sub2Group> Sub2Groups { get; set; } = new List<Sub2Group>();

        [NotMapped]
        public static SubGroup Empty { get; private set; } = new SubGroup();
    }
}
