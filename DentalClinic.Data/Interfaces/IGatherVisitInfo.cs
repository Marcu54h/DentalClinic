namespace DentalClinic.Data
{
    using DentalClinic.Data;
    using System;

    public interface IGatherVisitInfo
    {
        IVisitInfo ScanVisit(IVisitData visitData);
    }
}
