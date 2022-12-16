namespace DentalClinic.Data
{

    using System;

    public interface IVisitData
    {
        int Id { get; set; }

        DateTime Date { get; set; }

        IOfficeData OfficeData { get; set; }

        IProvideEmployeeData EmployeeData { get; set; }

        IProvidePatientData PatientData { get; set; }
    }
}