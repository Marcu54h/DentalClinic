namespace WebModel
{
    public class Sub2Treatment : EntityBase
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? SubTreatmentId { get; set; }

        public SubTreatment SubTreatment { get; set; } = SubTreatment.Empty;
    }
}
