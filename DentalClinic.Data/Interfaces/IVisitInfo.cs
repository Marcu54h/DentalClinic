using System.Collections.Generic;

namespace DentalClinic.Data
{
    public interface IVisitInfo
    {
        IProvidePatientData Patient { get; set; }
        ICollection<Comment> Comments { get; set; }
        ICollection<Tooth> Teeth { get; set; }
        ICollection<Treatment> Treatments { get; set; }
    }
}