namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IVisitCollection
    {
        ICollection<IVisitData> VisitCollection { get; }
    }
}
