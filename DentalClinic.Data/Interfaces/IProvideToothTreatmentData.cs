namespace DentalClinic.Data
{

    using System.Collections.Generic;

    internal interface IProvideToothTreatmentData
    {
        ICollection<IProvideTreatmentData> ToothTreatments(IProvideToothData tooth);
    }
}