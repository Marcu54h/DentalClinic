namespace DentalClinic.Data
{


    /// <summary>
    /// 
    /// </summary>
    public class PatientWrapper : IProvidePatientData
    {
        public string Caption { get { return Title + " " + FirstName + " " + LastName; } }

        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalNumber { get; set; }

        public bool? Insured { get; set; }

        public string InsuranceNumber { get; set; }

        public bool? Disabled { get; set; }

        public string DisabilityType { get; set; }

        public int? PersonId { get; set; }

        public int? EmployeeId { get; set; }

        public PatientWrapper()
        {

        }

        public PatientWrapper(Patient patient)
        {
            Id = patient.Id;
            Title = patient.Person.Title;
            FirstName = patient.Person.FirstName;
            LastName = patient.Person.LastName;
            PersonalNumber = patient.Person.PersonalNumber;
            Insured = patient.Insured;
            InsuranceNumber = patient.InsuranceNumber;
            Disabled = patient.Disabled;
            DisabilityType = patient.DisabilityType;
            PersonId = patient.Person.Id;
            EmployeeId = patient.EmployeeId;
        }

        public IProvidePatientData Interface => this;

    }
}
