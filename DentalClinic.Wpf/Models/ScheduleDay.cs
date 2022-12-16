namespace DentalClinic.Wpf
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DentalClinic.Data;
    using DentalClinic.Windows;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleDay : IScheduleDay
    {
        #region Properties

        public DateTime Date { get; set; } = DateTime.Today;

        public int NumberOfVisits => VisitsInDate.Count();

        public ICollection<Visit> VisitsInDate => MainDataContext.MainContext.Visits.Where(x => x.Date.Year == Date.Year && x.Date.Month == Date.Month && x.Date.Day == Date.Day).ToArray();

        public NotifyTaskCompletion<List<Visit>> VisitsInDateAsync
        {
            get
            {

                return new NotifyTaskCompletion<List<Visit>>(MainDataContext.MainContext.Visits.Where(x => x.Date.Year == Date.Year && x.Date.Month == Date.Month && x.Date.Day == Date.Day).ToListAsync());

            }
        }
            

        public string FirstVisitTime => VisitsInDate.OrderBy(x => x.Date).Select(y => y.Date.ToString("HH:mm")).FirstOrDefault();

        public NotifyTaskCompletion<string> FirstVisitTimeAsync
        {
            get
            {
                return null;
            }
            
        }


        public string LastVisitTime => VisitsInDate.OrderByDescending(x => x.Date).Select(y => y.Date.ToString("HH:mm")).FirstOrDefault();

        public DayOfWeek WeekDay => Date.DayOfWeek;

        public int MonthDay => Date.Day;

        #endregion // Properties

        #region Constructors

        public ScheduleDay()
        {

        }

        #endregion // Constructors

        #region Methods



        #endregion // Methods
        
    }
}
