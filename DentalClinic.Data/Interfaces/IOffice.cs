namespace DentalClinic.Data
{

    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IOffice
    {
        ICollection<IOfficeData> Offices { get; set; }
        IOfficeData GetOffice(IOfficeData officeData);
        void RemoveOffice(IOfficeData officeData);
        void UpateOffice(IOfficeData officeData);
    }
}
