namespace DentalClinic.Data
{

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface IVisit
    {
        ICollection<IVisitData> Visits { get; set; }
        void Schedule(IVisitData visitData);
        void UnSchedule(IVisitData visitData);
        void Update(IVisitData visitData);

        void SaveVisit(IVisitData visitData, ICollection<Treatment> treatments, ICollection<Comment> comments, ICollection<Tooth> teeth);
    }
}
