namespace DentalClinic.Data
{

    using System;

    /// <summary>
    /// 
    /// </summary>
    public class VisitWrapper : IVisitData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public IOfficeData OfficeData { get; set; }
        public IProvideEmployeeData EmployeeData { get; set; }
        public IProvidePatientData PatientData { get; set; }

        public VisitWrapper() { }

        public VisitWrapper(Visit visit, Employee employee, Patient patient, Office office)
        {
            Id = visit.Id;
            Date = visit.Date;
            OfficeData = office is null ? null : new OfficeWrapper(office).Interface;
            EmployeeData = employee is null ? null : new EmployeeWrapper(employee);
            PatientData = patient is null ? null : new PatientWrapper(patient);
        }

        public VisitWrapper(IProvideEmployeeData employeeData, IProvidePatientData patientData, IOfficeData officeData)
        {
            EmployeeData = employeeData;
            OfficeData = officeData;
            PatientData = patientData;
        }

        public IVisitData Interface => this;

        
    }
}
