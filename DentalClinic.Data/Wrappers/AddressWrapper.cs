namespace DentalClinic.Data
{

    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 
    /// </summary>
    public class AddressWrapper : IProvideAddressData
    {
        public int Id { get; set; }
        public string AddressType { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string StateProvince { get; set; }
        public string CountryRegion { get; set; }
        public string PostalCode { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public int PersonId { get; set; }

        public AddressWrapper()
        {

        }

        public AddressWrapper(Address address)
        {
            Id = address.Id;
            AddressType = address.AddressType;
            City = address.City;
            Street = address.Street;
            HouseNumber = address.HouseNumber;
            FlatNumber = address.FlatNumber;
            StateProvince = address.StateProvince;
            CountryRegion = address.CountryRegion;
            ModifiedDate = address.ModifiedDate;
            Email = address.Email;
            HomePhone = address.HomePhone;
            WorkPhone = address.WorkPhone;
            CellPhone = address.CellPhone;
            PersonId = address.PersonId;
        }

        public IProvideAddressData Interface => this;
    }
}
