namespace DentalClinic.Data
{

    /// <summary>
    /// 
    /// </summary>
    public interface IProvideAddressData
    {
        int Id { get; set; }
        string AddressType { get; set; }
        string City { get; set; }
        string Street { get; set; }
        string HouseNumber { get; set; }
        string FlatNumber { get; set; }
        string StateProvince { get; set; }
        string CountryRegion { get; set; }
        string PostalCode { get; set; }
        System.DateTime ModifiedDate { get; set; }
        string Email { get; set; }
        string HomePhone { get; set; }
        string WorkPhone { get; set; }
        string CellPhone { get; set; }
        int PersonId { get; set; }
    }
}
