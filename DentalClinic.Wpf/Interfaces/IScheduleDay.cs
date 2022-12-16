namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using DentalClinic.Windows;
    using System;
    using System.Collections.Generic;

    public interface IScheduleDay
    {
        DateTime Date { get; set; }
        int NumberOfVisits { get; }
        ICollection<Visit> VisitsInDate { get; }

        NotifyTaskCompletion<List<Visit>> VisitsInDateAsync { get; }

        string FirstVisitTime { get; }

        string LastVisitTime { get; }
        DayOfWeek WeekDay { get; }
        int MonthDay { get; }
    }
}