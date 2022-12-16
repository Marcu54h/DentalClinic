

namespace DentalClinic.Wpf
{

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    public class ScheduleControlViewModel : ObservableCollection<IScheduleDay>, IScheduleInfo
    {
        #region Fields

        private ICommand commandToDoubleClick;

        private ICommand commandToNextMonth;

        private ICommand commandToPreviousMonth;

        private ICommand commandToToday;

        private int month = DateTime.Today.Month;

        private int year = DateTime.Today.Year;

        #endregion // Fields

        #region Constructor

        public ScheduleControlViewModel()
        {
            SelectedScheduleDay = App.container.Resolve<IScheduleDay>();

            RefreshSchedule();
        }

        #endregion // Constructor

        #region Properties

        public DateTime DateTimePickerDate
        {
            get { return SelectedScheduleDay.Date; }
            set
            {
                SelectedScheduleDay.Date = value;

                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(DateTimePickerDate)));

                countDaysShift();

                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(DayShift)));

                countNumberOfRows();

                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(NumberOfRows)));

                setCalendar();

                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Month)));
            }
        }

        public int DayShift
        {
            get { return countDaysShift(); }
        }

        public IScheduleDay SelectedScheduleDay
        {
            get
            {
                return App.container.Resolve<Container>().SelectedScheduleDay;
            }
            set
            {
                App.container.Resolve<Container>().SelectedScheduleDay = value;

                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(SelectedScheduleDay)));
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(DateTimePickerDate)));
            }
        }
                       
        public string Month
        {
            get
            {
                return (new DateTime(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month, 1)).ToString("MMMM yyyy");
            }
        }

        public int NumberOfRows
        {
            get
            {
                return countNumberOfRows();
            }
        }

        #endregion // Properties

        #region ICommands

        public ICommand CommandToDoubleClick
        {
            get
            {
                if (commandToDoubleClick == null)
                    commandToDoubleClick = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().Call<ScheduleDayControl, ScheduleControl>();
                    });
                return commandToDoubleClick;
            }
        }

        public ICommand CommandToNextMonth
        {
            get
            {
                if (commandToNextMonth == null)
                    commandToNextMonth = new ActionCommand(d =>
                    {
                        substractMonths(1);
                    });
                return commandToNextMonth;
            }
        }

        public ICommand CommandToPreviousMonth
        {
            get
            {
                if (commandToPreviousMonth == null)
                    commandToPreviousMonth = new ActionCommand(d =>
                    {
                        substractMonths(-1);
                    });
                return commandToPreviousMonth;
            }
        }

        public ICommand CommandToToday
        {
            get
            {
                if (commandToToday is null)
                    commandToToday = new ActionCommand(x =>
                    {
                        SelectedScheduleDay.Date = DateTime.Today;
                        OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(SelectedScheduleDay)));
                        countDaysShift();
                        OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(DayShift)));
                        countNumberOfRows();
                        OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(NumberOfRows)));
                        setCalendar();
                        OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Month)));
                    });
                return commandToToday;
            }
        }

        #endregion // ICommands

        #region Methods

        private int countDaysShift()
        {
            switch (new DateTime(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month, 1).DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
                default:
                    return 0;
            }
        }

        private int countNumberOfRows()
        {
            if (DateTime.DaysInMonth(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month) == 
                30 && new DateTime(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month, 1).DayOfWeek == DayOfWeek.Sunday)
                return 6;
            if (DateTime.DaysInMonth(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month) ==
                31 && new DateTime(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month, 1).DayOfWeek == DayOfWeek.Saturday)
                return 6;
            if (DateTime.DaysInMonth(SelectedScheduleDay.Date.Year, month) ==
                31 && new DateTime(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month, 1).DayOfWeek == DayOfWeek.Sunday)
                return 6;
            return 5;
        }

        private void setCalendar()
        {
            DateTime selectedDay = SelectedScheduleDay.Date;

            Items.Clear();

            for (int i = 1; i <= DateTime.DaysInMonth(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month); i++)
            {
                IScheduleDay scheduleDay = App.container.Resolve<IScheduleDay>();

                scheduleDay.Date = new DateTime(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month, i);

                Add(scheduleDay);
            }

            if (DateTime.DaysInMonth(SelectedScheduleDay.Date.Year, SelectedScheduleDay.Date.Month) < DateTime.DaysInMonth(selectedDay.Year, selectedDay.Month))
                SelectedScheduleDay = Items.Last();
            else
                SelectedScheduleDay = Items.Where(x => x.Date.Day == selectedDay.Day).FirstOrDefault();

            CollectionViewSource.GetDefaultView(this).Refresh();
        }

        private void substractMonths(int months)
        {
            SelectedScheduleDay.Date = SelectedScheduleDay.Date.AddMonths(months);

            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(SelectedScheduleDay)));

            countDaysShift();

            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(DayShift)));

            countNumberOfRows();

            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(NumberOfRows)));

            setCalendar();

            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Month)));
            
        }

        public void RefreshSchedule()
        {
            setCalendar();
        }

        #endregion // Methods


    }
}
