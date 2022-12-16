

namespace DentalClinic.Data
{

    using System.Collections.Generic;

    public interface IPerformPatientOperation
    {
        ICollection<IProvidePatientData> GetPatientCollection();
        void AddPatient(IProvidePatientData patientData);
        void AddPatientWithAddresses(IProvidePatientData patientData, ICollection<IProvideAddressData> addresses);
        void DeletePatient(IProvidePatientData patientData);
        void UpdatePatient(IProvidePatientData patientData);
    }
}
