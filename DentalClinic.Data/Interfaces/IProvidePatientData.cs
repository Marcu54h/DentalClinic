namespace DentalClinic.Data
{
    public interface IProvidePatientData
    {
        string Caption { get; }
        int Id { get; }
        string Title { get; }
        string FirstName { get; }
        string LastName { get; }
        string PersonalNumber { get; }
        bool? Insured { get; }
        string InsuranceNumber { get; }
        bool? Disabled { get; }
        string DisabilityType { get; }
        int? PersonId { get; }
        int? EmployeeId { get; }
    }
}