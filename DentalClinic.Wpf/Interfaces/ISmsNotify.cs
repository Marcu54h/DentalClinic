namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface ISmsNotify
    {
        int? ShouldINotifyThisPerson(int personId);

        void ThisOneAgreed(int personId, int addressId);

        void ThisOneResigned(int personId);

        void NotifyThisOne(int personId, int addressId);

        void LoadFromXml();

        void SaveToXml();


    }
}
