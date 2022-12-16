using System.Collections.Generic;

namespace DentalClinic.Data
{
    public interface IProvideToothData
    {
        int Id { get; set; }

        string Number { get; set; }

        int VisitId { get; set; }

        ICollection<IProvideTreatmentData> Treatments { get; set; }
    }
}