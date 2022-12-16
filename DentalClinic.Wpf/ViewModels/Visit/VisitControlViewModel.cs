namespace DentalClinic.Wpf
{

    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class VisitControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand commandToAddVisit;

        private ICommand commandToDoubleClick;

        private ICommand commandToUnScheduleVisit;

        private string currentFilter = "All";
        private Visit selectedVisit = null;

        #endregion // Fields

        #region Properties

        public bool NewVisit { get; private set; } = false;

        public ICollection<Visit> FilteredVisitCollection { get; set; } = new ObservableCollection<Visit>();

        public ICollection<Visit> VisitCollection
        {
            get {
                return MainDataContext.MainContext.Visits.ToArray();
            }
        }


        public Visit SelectedVisit
        {
            get => selectedVisit;

            set
            {
                selectedVisit = value;
                NotifyPropertyChanged(nameof(SelectedVisit));
            }
        }

        #endregion // Properties

        #region Constructors

        public VisitControlViewModel()
        {
            selectedVisit = App.container.Resolve<Container>().SelectedVisit;
            Refresh();
        }

        #endregion // Constructors


        #region ICommands

        public ICommand CommandToScheduleVisit
        {
            get
            {
                if (commandToAddVisit is null)
                    commandToAddVisit = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().Call<AddVisitControl, VisitControl>();
                    });
                return commandToAddVisit;
            }
        }

        public ICommand CommandToDoubleClick
        {
            get
            {
                if (commandToDoubleClick == null)
                    commandToDoubleClick = new ActionCommand(x =>
                    {
                        if (!(SelectedVisit is null))
                        {
                            try
                            {
                                App.container.Resolve<Container>().SelectedTeeth.Clear();
                            }
                            catch { }

                            Visit v = MainDataContext.MainContext.Visits
                                                                 .Include("Comments")
                                                                 .Include("Treatments")
                                                                 .Include("Treatments.SubTreatment")
                                                                 .Include("Treatments.SubTreatment.Sub2Treatment")
                                                                 .Include("Teeth")
                                                                 .Include("Teeth.Comments")
                                                                 .Include("Teeth.Treatments")
                                                                 .Include("Teeth.Treatments.SubTreatment")
                                                                 .Include("Teeth.Treatments.SubTreatment.Sub2Treatment")
                                                                 .Include("Patient")
                                                                 .Include("Patient.Person")
                                                                 .Include("Employee")
                                                                 .Include("Employee.Person")
                                                                 .Include("Office")
                                                                 .Where(y => y.Id == ((Visit)x).Id)
                                                                 .FirstOrDefault();

                            App.container.Resolve<Container>().SelectedVisit = (v);

                            App.container.Resolve<Container>().SelectedPatient = (v.Patient);

                            App.container.Resolve<IKnowWhoCall>().Call<VisitViewControl, VisitControl>();
                        }

                    });
                return commandToDoubleClick;
            }
        }


        public ICommand CommandToUnScheduleVisit
        {
            get
            {
                if (commandToUnScheduleVisit is null)
                    commandToUnScheduleVisit = new ActionCommand(x =>
                    {
                        if (!(SelectedVisit is null))
                        {
                            MessageBoxResult result = MessageBox.Show("Czy napewno chcesz odwołać wizytę?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                if(FilteredVisitCollection.Contains(x as Visit))
                                {
                                    FilteredVisitCollection.Remove(x as Visit);
                                }

                                MainDataContext.MainContext.Visits.Remove(x as Visit);

                                MainDataContext.MainContext.SaveChanges();
                            }

                        }
                    });
                return commandToUnScheduleVisit;
            }
        }


        #endregion // ICommands

        #region Methods

        public void Refresh()
        {
            FilteredVisitCollection.Clear();

            if (currentFilter == "All")
            {
                FilteredVisitCollection = new ObservableCollection<Visit>(VisitCollection);

                // await Task.Run(() => FilteredVisitCollection = new ObservableCollection<Visit>(VisitCollection));

                //foreach (Visit v in VisitCollection)
                //{
                //    v.filled = v.Treatments.Count + v.Teeth.Count + v.Comments.Count;
                //    FilteredVisitCollection.Add(v);
                //}
            }
            else
            {
                foreach (Visit v in VisitCollection)
                {
                    if (v.Patient.Person.LastName.StartsWith(currentFilter))
                    {
                        FilteredVisitCollection.Add(v);
                    }
                }
            }
        }

        #endregion // Methods
    }
}
