namespace DentalClinic.Data
{

    using System.Collections.Generic;

    public interface IProvideSubTreatmentData
    {
        int Id { get; }
        string Type { get; }
        string Description { get; }

        ICollection<IProvideSub2TreatmentData> Sub2Treatments { get; }
    }
}