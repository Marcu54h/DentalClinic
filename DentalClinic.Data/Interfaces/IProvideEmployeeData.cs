namespace DentalClinic.Data
{
    public interface IProvideEmployeeData
    {
        string Caption { get; }
        int Id { get; }
        string Title { get; }
        string FirstName { get; }
        string LastName { get; }
        string PersonalNumber { get; }
        string PWZNumber { get; }
        string FavoriteColor { get; }
    }
}