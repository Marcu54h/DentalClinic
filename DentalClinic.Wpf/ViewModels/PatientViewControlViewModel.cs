namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using DentalClinic.Windows;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class PatientViewControlViewModel : BaseViewModel
    {
        #region Fields

        private bool changesMade = false;

        private bool changesSaved = true;

        private bool smsNotify;

        private ICommand commandToAddAddress;

        private ICommand commandToAddPatientComment;

        private ICommand commandToChangeSelectedComment;

        private ICommand commandToGoBack;

        private ICommand commandToRemovePatientComment;

        private ICommand commandToSavePatientComments;

        private string patientBornDate;

        private string patientPhone;

        private string patienEmail;

        private string selectedTooth;

        #endregion // Fields

        #region Properties

        public bool ChangesMade
        {
            get { return changesMade; }
            set
            {
                changesMade = value;
                NotifyProperitesChanged(nameof(ChangesMade));
            }
        }

        public bool ChangesSaved
        {
            get { return changesSaved; }
            set
            {
                changesSaved = value;
                NotifyProperitesChanged(nameof(ChangesSaved)); }
        }

        public bool SmsNotify
        {
            get { return App.container.Resolve<ISmsNotify>().ShouldINotifyThisPerson(SelectedPatient.Person.Id) == null ? false : true; }
            set
            {
                if (value == true)
                {
                    try
                    {
                        App.container.Resolve<ISmsNotify>().ThisOneAgreed(SelectedPatient.Person.Id, SelectedPatient.Person.Addresses.First().Id);
                    }
                    catch
                    {
                        MessageBox.Show("Brak danych o telefonie wybranej osoby.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    App.container.Resolve<ISmsNotify>().ThisOneResigned(SelectedPatient.Person.Id);
                }
                    

                NotifyPropertyChanged(nameof(SmsNotify));
            }
        }

        public bool ThisIsMainHost
        {
            get { return App.container.Resolve<Container>().AppSettings.ThisIsMainHost; }
            set
            {
                App.container.Resolve<Container>().AppSettings.ThisIsMainHost = value;
                NotifyProperitesChanged(nameof(ThisIsMainHost));
            }
        }

        public ICollection<Address> PatientAddresses
        {
            get { return SelectedPatient.Person.Addresses; }
            set { SelectedPatient.Person.Addresses = value; }
        }

        public ICollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();

        public ICollection<Comment> PatientComments
        {
            get { return SelectedPatient.Comments; }
            set { SelectedPatient.Comments = value; ChangesSaved = false; ChangesMade = true; }
        }

        public ICollection<TreatmentRow> PatientTreatments { get; set; } = new ObservableCollection<TreatmentRow>();

        public Patient SelectedPatient
        {
            get { return App.container.Resolve<Container>().SelectedPatient; }
            set
            {
                NotifyPropertyChanged(nameof(SelectedPatient));

                try
                {
                    PatientBornDate = (bool)App.container.Resolve<IPesel>().IsMale(SelectedPatient.Person.PersonalNumber) ?
                    "Urodzony : " + ((DateTime)App.container.Resolve<IPesel>().DateOfBirth(SelectedPatient.Person.PersonalNumber)).ToLongDateString() :
                    "Urodzona : " + ((DateTime)App.container.Resolve<IPesel>().DateOfBirth(SelectedPatient.Person.PersonalNumber)).ToLongDateString();
                }
                catch
                {
                    PatientBornDate = "Nie wprowadzono numeru PESEL lub adres nie prawidłowy";
                }

                

                PatientPhone = PatientAddresses.Count > 0 ? !string.IsNullOrWhiteSpace(PatientAddresses.First().CellPhone) ?
                                "Telefon: " + PatientAddresses.First().CellPhone : "Nie wprowadzono telefonu" : "Nie wprowadzono telefonu";

                PatientEmail = PatientAddresses.Count > 0 ? !string.IsNullOrWhiteSpace(PatientAddresses.First().Email) ?
                                "E-mail: " + PatientAddresses.First().Email : "Nie wprowadzono adresu e-mail" : "Nie wprowadzono adresu e-mail";
            }
        }


        public string PatientBornDate
        {
            get { return patientBornDate; }
            set { patientBornDate = value; NotifyPropertyChanged(nameof(PatientBornDate)); }
        }

        public string PatientPhone
        {
            get { return patientPhone; }
            set { patientPhone = value; NotifyPropertyChanged(nameof(PatientPhone)); }
        }

        public string PatientEmail
        {
            get { return patienEmail; }
            set { patienEmail = value; NotifyPropertyChanged(nameof(PatientEmail)); }
        }

        public TreatmentRow SelectedTreatment { get; set; }

        #endregion // Properties

        #region Constructors

        public PatientViewControlViewModel()
        {
            PatientAddresses = new ObservableCollection<Address>(PatientAddresses);

            PatientComments = new ObservableCollection<Comment>(SelectedPatient.Comments);

            foreach (TreatmentRow treatmentRow in new TreatmentRows(SelectedPatient).TreatmentList)
            {
                PatientTreatments.Add(treatmentRow);
            }

            try
            {
                PatientBornDate = (bool)App.container.Resolve<IPesel>().IsMale(SelectedPatient.Person.PersonalNumber) ?
                "Urodzony : " + ((DateTime)App.container.Resolve<IPesel>().DateOfBirth(SelectedPatient.Person.PersonalNumber)).ToLongDateString() :
                "Urodzona : " + ((DateTime)App.container.Resolve<IPesel>().DateOfBirth(SelectedPatient.Person.PersonalNumber)).ToLongDateString();
            }
            catch
            {
                PatientBornDate = "Nie wprowadzono numeru PESEL lub adres nie prawidłowy";
            }

            PatientPhone = PatientAddresses.Count > 0 ? !string.IsNullOrWhiteSpace(PatientAddresses.First().CellPhone) ?
                                "Telefon: " + PatientAddresses.First().CellPhone : "Nie wprowadzono telefonu" : "Nie wprowadzono telefonu";

            PatientEmail = PatientAddresses.Count > 0 ? !string.IsNullOrWhiteSpace(PatientAddresses.First().Email) ?
                            "E-mail: " + PatientAddresses.First().Email : "Nie wprowadzono adresu e-mail" : "Nie wprowadzono adresu e-mail";
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandToAddAddress
        {
            get
            {
                if (commandToAddAddress is null)
                    commandToAddAddress = new ActionCommand(x => PatientAddresses.Add(new Address()));
                return commandToAddAddress;
            }
        }

        public ICommand CommandToAddPatientComment
        {
            get
            {
                if (commandToAddPatientComment is null)
                    commandToAddPatientComment = new ActionCommand(x =>
                    {
                        PatientComments.Add(new Comment { Title = "Notatka - " + SelectedPatient.Person.Title + " " + SelectedPatient.Person.LastName + 
                                                                  " " + SelectedPatient.Person.FirstName + " - " + DateTime.Now.ToShortDateString(),
                                                                  Content = "Treść..."});
                        ChangesSaved = false;
                        ChangesMade = true;
                        
                    });
                return commandToAddPatientComment;
            }
        }

        public ICommand CommandToChangeSelectedComment
        {
            get
            {
                if (commandToChangeSelectedComment is null)
                    commandToChangeSelectedComment = new ActionCommand(x =>
                    {
                        if(!(x is null))
                        {
                            if ((x as TreatmentRow).ObjectOfTreatment != "wizyta")
                            {
                                if (selectedTooth != (x as TreatmentRow).ObjectOfTreatment)
                                {
                                    Comments.Clear();

                                    foreach (Comment comment in
                                                App.container.Resolve<ICommentOperator>()
                                                             .GetToothComments(SelectedPatient.Id, (x as TreatmentRow).ObjectOfTreatment))
                                    {
                                        Comments.Add(comment);
                                    }
                                    selectedTooth = (x as TreatmentRow).ObjectOfTreatment;
                                }
                                //CollectionViewSource.GetDefaultView(Comments).Refresh();
                            }
                            else if ((x as TreatmentRow).ObjectOfTreatment == "wizyta")
                            {
                                if (selectedTooth != (x as TreatmentRow).ObjectOfTreatment)
                                {
                                    Comments.Clear();

                                    foreach (Comment comment in
                                                App.container.Resolve<ICommentOperator>()
                                                             .GetPatientComments(SelectedPatient.Id))
                                    {
                                        Comments.Add(comment);
                                    }
                                    selectedTooth = (x as TreatmentRow).ObjectOfTreatment;
                                }
                                
                            }
                            else
                            {
                                Comments.Clear();
                                selectedTooth = null;
                            }
                        }
                        else
                        {
                            Comments.Clear();
                            selectedTooth = null;
                        }
                    });
                return commandToChangeSelectedComment;
            }
        }

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                    commandToGoBack = new ActionCommand(x => App.container.Resolve<IKnowWhoCall>().WhoCalledMe());
                return commandToGoBack;
            }
        }

        public ICommand CommandToRemovePatientComment
        {
            get
            {
                if (commandToRemovePatientComment is null)
                    commandToRemovePatientComment = new ActionCommand(x =>
                    {
                        if (!(x is null))
                        {
                            SelectedPatient.Comments.Remove(x as Comment);

                            MainDataContext.MainContext.SaveChanges();
                        }
                    });
                return commandToRemovePatientComment;
            }
        }

        public ICommand CommandToSavePatientComments
        {
            get
            {
                if (commandToSavePatientComments is null)
                    commandToSavePatientComments = new ActionCommand(x =>
                    {
                        MainDataContext.MainContext.SaveChanges();

                        ChangesMade = false;

                        ChangesSaved = true;
                    });
                return commandToSavePatientComments;
            }
        }

        #endregion // ICommands

        #region Methods



        #endregion // Methods
    }


    public class TreatmentRows
    {
        private Patient patient;

        public ICollection<TreatmentRow> TreatmentList { get; }

        public TreatmentRows(Patient patient)
        {
            this.patient = patient;

            TreatmentList = new Collection<TreatmentRow>();

            getTreatments();
        }

        private void getTreatments()
        {
           
                  patient.Visits.ToList().ForEach(visit =>
                  {
                      if (visit.Treatments.Count > 0)
                          foreach (Treatment treatment in visit.Treatments)
                              if (treatment.SubTreatment.Count > 0)
                                  foreach (SubTreatment sub in treatment.SubTreatment)
                                      if (sub.Sub2Treatment.Count > 0)
                                          foreach (Sub2Treatment sub2 in sub.Sub2Treatment)
                                          {
                                              TreatmentList.Add(new TreatmentRow
                                              {
                                                  VisitDateTime = visit.Date,
                                                  ObjectOfTreatment = "wizyta",
                                                  Treatment = treatment.Type,
                                                  SubTreatment = sub.Type,
                                                  Sub2Treatment = sub2.Type
                                              });
                                          }
                                      else
                                          TreatmentList.Add(new TreatmentRow
                                          {
                                              VisitDateTime = visit.Date,
                                              ObjectOfTreatment = "wizyta",
                                              Treatment = treatment.Type,
                                              SubTreatment = sub.Type,
                                          });
                              else
                                  TreatmentList.Add(new TreatmentRow
                                  {
                                      VisitDateTime = visit.Date,
                                      ObjectOfTreatment = "wizyta",
                                      Treatment = treatment.Type,
                                  });
                      else if (visit.Comments.Count > 0)
                          TreatmentList.Add(new TreatmentRow
                          {
                              VisitDateTime = visit.Date,
                              ObjectOfTreatment = "wizyta",
                          });

                      if (visit.Teeth.Count > 0)
                      {
                          foreach(Tooth tooth in visit.Teeth)
                              if (tooth.Treatments.Count > 0)
                                  foreach (Treatment treatment in tooth.Treatments)
                                      if (treatment.SubTreatment.Count > 0)
                                          foreach (SubTreatment sub in treatment.SubTreatment)
                                              if (sub.Sub2Treatment.Count > 0)
                                                  foreach (Sub2Treatment sub2 in sub.Sub2Treatment)
                                                  {
                                                      TreatmentList.Add(new TreatmentRow
                                                      {
                                                          VisitDateTime = visit.Date,
                                                          ObjectOfTreatment = tooth.Number,
                                                          Treatment = treatment.Type,
                                                          SubTreatment = sub.Type,
                                                          Sub2Treatment = sub2.Type
                                                      });
                                                  }
                                              else
                                                  TreatmentList.Add(new TreatmentRow
                                                  {
                                                      VisitDateTime = visit.Date,
                                                      ObjectOfTreatment = tooth.Number,
                                                      Treatment = treatment.Type,
                                                      SubTreatment = sub.Type,
                                                  });
                                      else
                                          TreatmentList.Add(new TreatmentRow
                                          {
                                              VisitDateTime = visit.Date,
                                              ObjectOfTreatment = tooth.Number,
                                              Treatment = treatment.Type,
                                          });
                               else
                                  TreatmentList.Add(new TreatmentRow
                                  {
                                      VisitDateTime = visit.Date,
                                      ObjectOfTreatment = tooth.Number
                                  });

                      }
                  });
        }
    }

    public class TreatmentRow
    {
        public DateTime VisitDateTime { get; set; }
        public string ObjectOfTreatment { get; set; }
        public string Treatment { get; set; }
        public string SubTreatment { get; set; }
        public string Sub2Treatment { get; set; }
    }
}
