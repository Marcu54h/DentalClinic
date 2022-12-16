namespace DentalClinic.Data
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IPatientInfo
    {
        Employee PatientEmployee { get; set; }
        ICollection<Address> Addresses { get; set; }
        ICollection<Visit> Visits { get; set; }
        ICollection<Comment> Comments { get; set; }
        PriceList PriceList { get; set; }
    }
}
