namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IPerformAddressOperation
    {
        void AddAddressToPerson(int personId, IProvideAddressData addressData);
        void RemoveAddressFromPerson(int personId, IProvideAddressData addressData);
        void UpdatePersonAddress(int personId, IProvideAddressData addressData);
        void ReplacePersonAddressCollection(int personId, ICollection<IProvideAddressData> addresses);

        ICollection<IProvideAddressData> PersonAddresses(int personId);
    }
}
