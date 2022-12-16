

namespace DentalClinic.Data
{

    using System.Collections.Generic;

    public interface IProvideTreatmentData
    {
        ICollection<Treatment> treatments { get; set; }
    }
}