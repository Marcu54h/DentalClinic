namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class AddressOp : IPerformAddressOperation
    {

        #region Methods

        public void AddAddressToPerson(int personId, IProvideAddressData addressData)
        {
            using (PDContainer pd = new PDContainer())
            {
                pd.Addresses.Add(new Address
                {
                    AddressType = addressData.AddressType,
                    City = addressData.City,
                    Street = addressData.Street,
                    HouseNumber = addressData.HouseNumber,
                    FlatNumber = addressData.FlatNumber,
                    StateProvince = addressData.StateProvince,
                    CountryRegion = addressData.CountryRegion,
                    PostalCode = addressData.PostalCode,
                    ModifiedDate = DateTime.Now,
                    Email = addressData.Email,
                    HomePhone = addressData.HomePhone,
                    WorkPhone = addressData.WorkPhone,
                    CellPhone = addressData.CellPhone,
                    PersonId = personId
                });
                pd.SaveChanges();
            }
        }

        public ICollection<IProvideAddressData> PersonAddresses(int personId)
        {
            using (PDContainer pd = new PDContainer())
            {
                ICollection<IProvideAddressData> addresses = new Collection<IProvideAddressData>();

                pd.Addresses.Where(y => y.PersonId == personId).ToList().ForEach(x =>
                {
                    addresses.Add(new AddressWrapper(x).Interface);
                });

                return addresses;
            }
        }

        public void RemoveAddressFromPerson(int personId, IProvideAddressData addressData)
        {
            using (PDContainer pd = new PDContainer())
            {
                try
                {
                    pd.Addresses.Remove(pd.Addresses.Where(x => x.Id == addressData.Id && x.PersonId == personId).FirstOrDefault());
                    pd.SaveChanges();
                }
                catch { }
            }
        }

        public void ReplacePersonAddressCollection(int personId, ICollection<IProvideAddressData> addresses)
        {
            using (PDContainer pd = new PDContainer())
            {
                pd.Addresses.Where(x => x.PersonId == personId).ToList().ForEach(y => pd.Addresses.Remove(y));

                addresses.ToList().ForEach(x =>
                {
                    pd.Addresses.Add(new Address
                    {
                        AddressType = x.AddressType,
                        City = x.City,
                        Street = x.Street,
                        HouseNumber = x.HouseNumber,
                        FlatNumber = x.FlatNumber,
                        StateProvince = x.StateProvince,
                        CountryRegion = x.CountryRegion,
                        PostalCode = x.PostalCode,
                        ModifiedDate = DateTime.Now,
                        Email = x.Email,
                        HomePhone = x.HomePhone,
                        WorkPhone = x.WorkPhone,
                        CellPhone = x.CellPhone,
                        PersonId = personId
                    });
                });
                pd.SaveChanges();
            }
        }

        public void UpdatePersonAddress(int personId, IProvideAddressData addressData)
        {
            using (PDContainer pd = new PDContainer())
            {
                Address address = pd.Addresses.Where(x => x.PersonId == personId && x.Id == addressData.Id).FirstOrDefault();

                if (!(address is null))
                {
                    address.AddressType = addressData.AddressType;
                    address.City = addressData.City;
                    address.Street = addressData.Street;
                    address.HouseNumber = addressData.HouseNumber;
                    address.FlatNumber = addressData.FlatNumber;
                    address.StateProvince = addressData.StateProvince;
                    address.CountryRegion = addressData.CountryRegion;
                    address.PostalCode = addressData.PostalCode;
                    address.ModifiedDate = DateTime.Now;
                    address.Email = addressData.Email;
                    address.HomePhone = addressData.HomePhone;
                    address.WorkPhone = addressData.WorkPhone;
                    address.CellPhone = addressData.CellPhone;

                    pd.SaveChanges();
                }
            }
        }

        #endregion // Methods

    }
}
