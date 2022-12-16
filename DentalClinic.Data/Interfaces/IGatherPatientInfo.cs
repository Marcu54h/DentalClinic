namespace DentalClinic.Data
{
    using DentalClinic.Data;
    using System;

    public interface IGatherPatientInfo
    {
        IPatientInfo ScanPatient(IProvidePatientData patientData);
    }
}
