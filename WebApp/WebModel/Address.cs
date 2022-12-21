using System.ComponentModel.DataAnnotations.Schema;

namespace WebModel
{
    public class Address : EntityBase
    {
        public string AddressType { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string FlatNumber { get; set; } = string.Empty;
        public string StateProvince { get; set; } = string.Empty;
        public string CountryRegion { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string WorkPhone { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public int PersonId { get; set; }

        [NotMapped]
        public static Address Empty { get; private set; } = new Address();
    }
}
