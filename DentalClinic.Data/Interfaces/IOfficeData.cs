namespace DentalClinic.Data
{

    public interface IOfficeData
    {
        int Id { get; set; }
        string Type { get; set; }
        string Number { get; set; }
        string Label { get; set; }

        string Caption { get; }
    }
}