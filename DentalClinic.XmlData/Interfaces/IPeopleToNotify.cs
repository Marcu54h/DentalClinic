namespace DentalClinic.XmlData
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IPeopleToNotify
    {
        ICollection<Visit> VisitsToAnounce { get; set; }
        ICollection<Visit> VisitsToAnounceAsync { get; set; }
    }
}
