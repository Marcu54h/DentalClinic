namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    public class FilesControlViewModel : BaseViewModel
    {

        #region Fields

        private ICommand commandToAddPatient;

        private ICommand commandToDeletePatient;

        private ICommand commandToDoubleClick;

        private ICommand commandToEditPatient;

        private ICommand commandToFilter;

        private ICommand commandToRefresh;

        private string filter = "";

        private int filterBy = 0; // LastName - "NAZWISKA"

        #endregion // Fields

        #region Constructor

        public FilesControlViewModel()
        {
            Refresh();
        }

        #endregion // Constructor

        #region Properties

        public ICollection<Patient> Patients
        {
            get
            {
                return MainDataContext.MainContext.Patients
                                                  .Include("Person")
                                                  .Include("Person.Addresses")
                                                  .Include("Employee")
                                                  .Include("Employee.Person")
                                                  .ToArray();
            }
        }

        public IList<Patient> FilteredPatientCollection { get; set; } = new ObservableCollection<Patient>();

        public Patient Patient { get; set; }

        public string Filter 
        {
            get { return filter; }
            set { filter = value.ToLower(); NotifyProperitesChanged(nameof(Filter)); Refresh(); }
        }

        public int FilterBy
        {
            get { return filterBy; }
            set { filterBy = value; NotifyProperitesChanged(nameof(FilterBy)); Refresh(); }
        }


        #endregion // Properties

        #region ICommand

        public ICommand CommandToAddPatient
        {
            get
            {
                if (commandToAddPatient == null)
                    commandToAddPatient = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().Call<AddPatientControl, FilesControl>();
                    });
                return commandToAddPatient;
            }
        }

        public ICommand CommandToDeletePatient
        {
            get
            {
                if (commandToDeletePatient is null)
                    commandToDeletePatient = new ActionCommand(x =>
                    {
                        MessageBoxResult result = MessageBox.Show("Czy napewno chcesz usunąć konto pacjetna?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                MainDataContext.MainContext.Patients.Remove(Patient);
                            }
                            catch
                            {

                            }
                            
                            FilteredPatientCollection.Remove(Patient);

                            MainDataContext.MainContext.SaveChanges();
                        }
                    });
                return commandToDeletePatient;
            }
        }

        public ICommand CommandToDoubleClick
        {
            get
            {
                if (commandToDoubleClick == null)
                    commandToDoubleClick = new ActionCommand(x =>
                    {
                        if (!(Patient is null))
                        {
                            Patient p = MainDataContext.MainContext.Patients
                                                                   .Include("Comments")
                                                                   .Include("Person")
                                                                   .Include("Employee")
                                                                   .Include("Employee.Person")
                                                                   .Include("Person.Addresses")
                                                                   .Include("Visits")
                                                                   .Include("Visits.Treatments")
                                                                   .Include("Visits.Treatments.SubTreatment")
                                                                   .Include("Visits.Treatments.SubTreatment.Sub2Treatment")
                                                                   .Include("Visits.Comments")
                                                                   .Include("Visits.Teeth")
                                                                   .Include("Visits.Teeth.Treatments")
                                                                   .Include("Visits.Teeth.Treatments.SubTreatment")
                                                                   .Include("Visits.Teeth.Treatments.SubTreatment.Sub2Treatment")
                                                                   .Include("Visits.Teeth.Comments")
                                                                   .Where(y => y.Id == Patient.Id)
                                                                   .FirstOrDefault();

                            App.container.Resolve<Container>().SelectedPatient = p;
                            App.container.Resolve<IKnowWhoCall>().Call<PatientViewControl, FilesControl>();
                        }
                            
                    });
                return commandToDoubleClick;
            }
        }

        public ICommand CommandToEditPatient
        {
            get
            {
                if (commandToEditPatient == null)
                    commandToEditPatient = new ActionCommand(x =>
                    {
                        if (!(Patient is null))
                        {

                            Patient p = MainDataContext.MainContext.Patients
                                            .Include("Person")
                                            .Include("Employee")
                                            .Include("Employee.Person")
                                            .Include("Person.Addresses")
                                            .Where(y => y.Id == Patient.Id)
                                            .FirstOrDefault();

                            App.container.Resolve<Container>().SelectedPatient = p;

                            App.container.Resolve<IKnowWhoCall>().Call<EditPatientControl, FilesControl>();
                        }
                    });
                return commandToEditPatient;
            }
        }

        public ICommand CommandToFilter
        {
            get
            {
                if (commandToFilter is null)
                    commandToFilter = new ActionCommand(x =>
                    {
                        if (Filter != (string)x)
                        {
                            Filter = (string)x;
                            Refresh();
                        }
                        
                    });
                return commandToFilter;
            }
        }

        public ICommand CommandToRefresh
        {
            get
            {
                if (commandToRefresh == null)
                    commandToRefresh = new ActionCommand(x => Refresh());
                return commandToRefresh;
            }
        }

        
        #endregion // ICommand

        #region Methods

        public void Refresh()
        {
            FilteredPatientCollection.Clear();

            if (string.IsNullOrWhiteSpace(Filter))
            {
                Patients.ToList().ForEach(x => FilteredPatientCollection.Add(x));
            }
            else
            {
                switch(FilterBy)
                {
                    case 0 :
                        try { Patients.ToList().FindAll(x => x.Person.LastName.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                    case 1:
                        try { Patients.ToList().FindAll(x => x.Person.FirstName.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                    case 2:
                        try { Patients.ToList().FindAll(x => x.Person.PersonalNumber.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                    case 3:
                        try { Patients.ToList().FindAll(x => x.Person.Title.ToLower().StartsWith(Filter)).ForEach(y => FilteredPatientCollection.Add(y)); } catch { }
                        break;
                }
            }
        }

        #endregion // Methods
    }
}
