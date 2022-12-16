namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
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
    public class VisitViewControlViewModel : BaseViewModel
    {
        #region Fields

        private bool changesHasBeenMade = false;

        private bool changesHasBeenSaved = true;

        private bool deciduousTeeth = false;

        private bool newVisitDateTime = false;

        private bool permanentTeeth = true;

        private DateTime newVisitDate;

        private ICommand commandToAddTreatment;

        private ICommand commandToAddToothComment;

        private ICommand commandToAddVisitComment;

        private ICommand commandToClick;

        private ICommand commandToGoBack;

        private ICommand commandToRemoveToothComment;

        private ICommand commandToRemoveToothTreatment;

        private ICommand commandToRemoveVisitTreatment;

        private ICommand commandToRemoveVisitComment;

        private ICommand commandToSaveChanges;

        private ICollection<Treatment> patientTreatmens = new Collection<Treatment>();

        private int newVisitHour;

        private int newVisitMinute;

        private string treatmentBinding;

        private string visitDateLabel;

        #endregion // Fields

        #region Properties

        public bool ChangesHasBeenMade
        {
            get { return changesHasBeenMade; }
            set
            {
                changesHasBeenMade = value;
                NotifyPropertyChanged(nameof(ChangesHasBeenMade));
            }
        }

        public bool ChangesHasBeenSaved
        {
            get { return changesHasBeenSaved; }
            set { changesHasBeenSaved = value; NotifyPropertyChanged(nameof(ChangesHasBeenSaved)); }
        }

        public bool DeciduousTeeth
        {
            get { return deciduousTeeth; }
            set { deciduousTeeth = value; NotifyPropertyChanged(nameof(DeciduousTeeth)); }
        }

        public bool PermanentTeeth
        {
            get { return permanentTeeth; }
            set { permanentTeeth = value; NotifyPropertyChanged(nameof(PermanentTeeth)); }
        }

        public DateTime NewVisitDate
        {
            get { return newVisitDate; }
            set
            {
                newVisitDate = value;

                newVisitDateTime = NewVisitDateTimeSet();

                NotifyPropertyChanged(nameof(NewVisitDate));
            }
        }

        public Employee SelectedEmployee
        {
            get
            {
                return SelectedVisit.Employee;
            }
            set
            {
                if (!(value is null))
                    SelectedVisit.Employee = value;
            }
        }

        public ICollection<Comment> VisitComments
        {
            get { return SelectedVisit.Comments; }
            set { SelectedVisit.Comments = value; }
        }

        public ICollection<Employee> EmployeeCollection
        {
            get
            {
                return MainDataContext.MainContext.Employees
                                                  .Include("Person")
                                                  .OrderBy(x => x.Person.LastName)
                                                  .ToArray();
            }
        }

        public ICollection<int> HourCombo { get; } = Enumerable.Range
            (
                App.container.Resolve<Container>().AppSettings.StartWorkHour,
                App.container.Resolve<Container>().AppSettings.EndWorkHour - App.container.Resolve<Container>().AppSettings.StartWorkHour + 1
            ).ToArray();

        public ICollection<int> MinuteCombo { get; } = new Collection<int>();

        public ICollection<Tooth> VisitTeeth
        {
            get { return SelectedVisit.Teeth; }
            set { SelectedVisit.Teeth = value; }
        }

        public ICollection<Treatment> VisitTreatments
        {
            get { return SelectedVisit.Treatments; }
            set { SelectedVisit.Treatments = value; }
        }

        public ICollection<Treatment> TreatmentCollection
        {
            get
            {
                return MainDataContext.MainContext.Treatments
                                                             .Include("SubTreatment")
                                                             .Include("SubTreatment.Sub2Treatment")
                                                             .Where(x => x.ToothId == null && x.VisitId == null)
                                                             .OrderBy(x => x.Type)
                                                             .ToArray();
            }
        }

        public int NewVisitHour
        {
            get { return newVisitHour; }
            set
            {
                newVisitHour = value;

                newVisitDateTime = NewVisitDateTimeSet();

                NotifyPropertyChanged(nameof(NewVisitHour));
            }
        }

        public int NewVisitMinute
        {
            get { return newVisitMinute; }
            set
            {
                newVisitMinute = value;

                newVisitDateTime = NewVisitDateTimeSet();

                NotifyPropertyChanged(nameof(NewVisitMinute));
            }
        }

        public Patient SelectedPatient
        {
            get { return App.container.Resolve<Container>().SelectedPatient; }
            set { App.container.Resolve<Container>().SelectedPatient = value; }
        }

        public Visit SelectedVisit
        {
            get { return App.container.Resolve<Container>().SelectedVisit; }
            set
            {
                VisitDateLabel = SelectedPatient.Person.Title + " " + SelectedPatient.Person.FirstName + " " +
                                 SelectedPatient.Person.LastName + ", dn. " + SelectedVisit.Date.ToShortDateString() +
                                 " o godz." + SelectedVisit.Date.Hour + ":" + SelectedVisit.Date.Minute;

                VisitComments.Clear();
                SelectedVisit.Comments.ToList().ForEach(x => VisitComments.Add(x));

                VisitTeeth.Clear();
                SelectedVisit.Teeth.ToList().ForEach(x => VisitTeeth.Add(x));

                VisitTreatments.Clear();
                SelectedVisit.Treatments.ToList().ForEach(x => VisitTreatments.Add(x));

            }
        }

        public WorkInterval SelectedWorkInterval
        {
            get
            {
                return App.container.Resolve<Container>().SelectedWorkInterval;
            }
            set
            {
                App.container.Resolve<Container>().SelectedWorkInterval = value;
                NotifyPropertyChanged(nameof(SelectedWorkInterval));
            }
        }

        public string TreatmentBinding
        {
            get
            {
                if (treatmentBinding is null)
                    return null;
                if (treatmentBinding == "patient")
                {
                    return "do pacjenta";
                }
                else
                {
                    if (DeciduousTeeth)
                        return "do zęba nr " + (int.Parse(treatmentBinding) + 40).ToString();
                    else
                        return "do zęba nr " + treatmentBinding;
                }
            }
            set
            {
                treatmentBinding = value;
                NotifyPropertyChanged(nameof(TreatmentBinding));
            }
        }

        public string VisitDateLabel
        {
            get
            {
                return App.container.Resolve<Container>().SelectedVisit.Date.ToString("HH:mm dddd, dd.MM.yyyy") +
                       " - " + App.container.Resolve<Container>().SelectedPatient.Person.Title + " " +
                       App.container.Resolve<Container>().SelectedPatient.Person.FirstName + " " +
                       App.container.Resolve<Container>().SelectedPatient.Person.LastName;
            }
            set { visitDateLabel = value; NotifyPropertyChanged(nameof(VisitDateLabel)); }
        }

        #endregion // Properties

        #region Constructors


        public VisitViewControlViewModel()
        {
            VisitComments = new ObservableCollection<Comment>(SelectedVisit.Comments);

            VisitTeeth = new ObservableCollection<Tooth>(SelectedVisit.Teeth.Where(x => x.Comments.Count > 0 || x.Treatments.Count > 0));

            VisitTreatments = new ObservableCollection<Treatment>(SelectedVisit.Treatments);

            for (int i = 0; i < 60; i += App.container.Resolve<Container>().AppSettings.ScheduleInterval) MinuteCombo.Add(i);

            NewVisitHour = SelectedVisit.Date.Hour;

            NewVisitMinute = SelectedVisit.Date.Minute;

            NewVisitDate = SelectedVisit.Date;
        }


        #endregion // Constructors

        #region ICommands

        public ICommand CommandToAddTreatment
        {
            get
            {
                if (commandToAddTreatment is null)
                    commandToAddTreatment = new ActionCommand(x =>
                    {

                            if (App.container.Resolve<Container>().CommentPatient)
                            {
                                tryToAddTreatment(x, VisitTreatments);
                                CollectionViewSource.GetDefaultView(VisitTreatments).Refresh();
                            }
                            else
                            {
                                if (App.container.Resolve<Container>().SelectedTeeth.Count > 0)
                                {
                                    foreach(Tooth t in App.container.Resolve<Container>().SelectedTeeth)
                                    {
                                        Tooth visitTooth = VisitTeeth.Where(y => y.Number == t.Number).FirstOrDefault();

                                        if (visitTooth is null)
                                        {
                                            VisitTeeth.Add(t);
                                            tryToAddTreatment(x, t.Treatments);
                                        }
                                        else
                                        {
                                            tryToAddTreatment(x, visitTooth.Treatments);
                                        }
                                    }
                                    CollectionViewSource.GetDefaultView(VisitTeeth).Refresh();
                                }
                            }

                        ChangesHasBeenMade = true;
                        ChangesHasBeenSaved = false;
                    });
                return commandToAddTreatment;
            }
        }

        public ICommand CommandToAddToothComment
        {
            get
            {
                if (commandToAddToothComment is null)
                    commandToAddToothComment = new ActionCommand(x =>
                    {
                        if (App.container.Resolve<Container>().SelectedTeeth.Count > 0)
                        {
                            

                            foreach(Tooth tooth in App.container.Resolve<Container>().SelectedTeeth)
                            {
                                Tooth visitTooth = VisitTeeth.Where(y => y.Number == tooth.Number).FirstOrDefault();

                                Comment comment = new Comment
                                {
                                    Title = "Notatka do zęba nr " + tooth.Number.ToString() + " - " +
                                                                    SelectedVisit.Date.ToShortDateString(),
                                    Content = "Treść..."
                                };


                                if (visitTooth is null)
                                {
                                    VisitTeeth.Add(tooth);
                                    tooth.Comments.Add(comment);
                                }
                                else
                                {
                                    visitTooth.Comments.Add(comment);
                                }
                            }

                            CollectionViewSource.GetDefaultView(VisitTeeth).Refresh();
                        }
                        ChangesHasBeenMade = true;
                        ChangesHasBeenSaved = false;
                    });
                return commandToAddToothComment;
            }
        }

        public ICommand CommandToAddVisitComment
        {
            get
            {
                if (commandToAddVisitComment is null)
                    commandToAddVisitComment = new ActionCommand(x =>
                    {
                        VisitComments.Add(new Comment
                        {
                            Title = "Notatka do wizyty - " + SelectedPatient.Person.Title + " " 
                                    + SelectedPatient.Person.FirstName  + " " + 
                                    SelectedPatient.Person.LastName + " - " + SelectedVisit.Date.ToShortDateString(),
                            Content = "Treść notatki"
                        });
                        ChangesHasBeenMade = true;
                        ChangesHasBeenSaved = false;
                        NotifyPropertyChanged(nameof(VisitComments));
                        CollectionViewSource.GetDefaultView(VisitComments).Refresh();
                    });
            
                return commandToAddVisitComment;
            }

        }

        public ICommand CommandToClick
        {
            get
            {
                if (commandToClick is null)
                    commandToClick = new ActionCommand(x =>
                    {
                        TreatmentBinding = x.ToString();
                        MessageBox.Show(x as string);
                    });
                return commandToClick;
            }
        }

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                    commandToGoBack = new ActionCommand(x =>
                    {
                        if (ChangesHasBeenSaved)
                        {
                            App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show("Zmiany nie zostały zapisane i zostaną utracone jeżeli teraz wyjdziesz.",
                                                                      "Czy napewno wyjść?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                            }
                        }
                        App.container.Resolve<Container>().SelectedTeeth.Clear();
                    });
                return commandToGoBack;
            }
        }

        public ICommand CommandToRemoveToothComment
        {
            get
            {
                if (commandToRemoveToothComment is null)
                    commandToRemoveToothComment = new ActionCommand(x =>
                    {
                        VisitTeeth.ToList().ForEach(y =>
                        {
                            if (y.Comments.Contains(x as Comment))
                            {
                                y.Comments.Remove(x as Comment);
                                NotifyPropertyChanged(nameof(VisitTeeth));
                                CollectionViewSource.GetDefaultView(VisitTeeth).Refresh();
                                ChangesHasBeenMade = true;
                                ChangesHasBeenSaved = false;
                            }

                            if (!(y.Comments.Count > 0) && !(y.Treatments.Count > 0))
                            {
                                VisitTeeth.Remove(y);
                            }
                        });
                    });
                return commandToRemoveToothComment;
            }
        }

        public ICommand CommandToRemoveToothTreatment
        {
            get
            {
                if (commandToRemoveToothTreatment is null)
                    commandToRemoveToothTreatment = new ActionCommand(x =>
                    {
                        if (x is Sub2Treatment)
                        {
                            VisitTeeth.ToList().ForEach(t =>
                            {
                                t.Treatments.ToList().ForEach(y =>
                                {
                                    y.SubTreatment.ToList().ForEach(z =>
                                    {
                                        if (z.Sub2Treatment.Contains(x))
                                        {
                                            z.Sub2Treatment.Remove(x as Sub2Treatment);

                                            try
                                            {
                                                MainDataContext.MainContext.Sub2Treatments.Remove(x as Sub2Treatment);
                                            }
                                            catch
                                            {
                                                //MessageBox.Show("Nie powiodło się usuwanie zabiegu II z głównego kontekstu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                                            }
                                        }
                                    });
                                });

                                if (!(t.Comments.Count > 0) && !(t.Treatments.Count > 0))
                                {
                                    try
                                    {
                                        MainDataContext.MainContext.Teeth.Remove(t as Tooth);
                                    }
                                    catch { }

                                    VisitTeeth.Remove(t);
                                }
                            });
                        }

                        if (x is SubTreatment)
                        {
                            VisitTeeth.ToList().ForEach(t =>
                            {
                                t.Treatments.ToList().ForEach(y =>
                                {
                                    if (y.SubTreatment.Contains(x as SubTreatment))
                                    {
                                        y.SubTreatment.Remove(x as SubTreatment);

                                        try
                                        {
                                            MainDataContext.MainContext.SubTreatments.Remove(x as SubTreatment);
                                        }
                                        catch
                                        {
                                            //MessageBox.Show("Nie powiodło się usuwanie zabiegu z głównego kontekstu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                                });

                                if (!(t.Comments.Count > 0) && !(t.Treatments.Count > 0))
                                {
                                    try
                                    {
                                        MainDataContext.MainContext.Teeth.Remove(t as Tooth);
                                    }
                                    catch { }

                                    VisitTeeth.Remove(t);
                                }
                            });
                        }
                        if (x is Treatment)
                        {
                            VisitTeeth.ToList().ForEach(t =>
                            {
                                if (t.Treatments.Contains(x as Treatment))
                                {
                                    t.Treatments.Remove(x as Treatment);

                                    try
                                    {
                                        MainDataContext.MainContext.Treatments.Remove(x as Treatment);
                                    }
                                    catch
                                    {
                                        //MessageBox.Show("Nie powiodło się usuwanie grupy z głównego kontekstu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }


                                if (!(t.Comments.Count > 0) && !(t.Treatments.Count > 0))
                                {
                                    try
                                    {
                                        MainDataContext.MainContext.Teeth.Remove(t as Tooth);
                                    }
                                    catch { }

                                    VisitTeeth.Remove(t);
                                }
                            });
                        }

                        ChangesHasBeenMade = true;
                        ChangesHasBeenSaved = false;

                        CollectionViewSource.GetDefaultView(VisitTeeth).Refresh();
                    });
                return commandToRemoveToothTreatment;
            }
        }

        public ICommand CommandToRemoveVisitComment
        {
            get
            {
                if (commandToRemoveVisitComment is null)
                    commandToRemoveVisitComment = new ActionCommand(x =>
                    {
                        VisitComments.Remove(x as Comment);
                        ChangesHasBeenMade = true;
                        ChangesHasBeenSaved = false;
                        NotifyPropertyChanged(nameof(VisitComments));
                        CollectionViewSource.GetDefaultView(VisitComments).Refresh();
                    });
                return commandToRemoveVisitComment;
            }
        }

        public ICommand CommandToRemoveVisitTreatment
        {
            get
            {
                if (commandToRemoveVisitTreatment is null)
                    commandToRemoveVisitTreatment = new ActionCommand(x =>
                    {
                        if (x is Sub2Treatment)
                        {
                            VisitTreatments.ToList().ForEach(t =>
                            {
                                t.SubTreatment.ToList().ForEach(y =>
                                {
                                    if (y.Sub2Treatment.Contains(x))
                                    {
                                        try
                                        {
                                            MainDataContext.MainContext.Sub2Treatments.Remove(x as Sub2Treatment);
                                        }
                                        catch { }

                                        y.Sub2Treatment.Remove(x as Sub2Treatment);
                                    }
                                });
                            });
                        }

                        if (x is SubTreatment)
                        {
                            VisitTreatments.ToList().ForEach(t =>
                            {
                                if (t.SubTreatment.Contains(x as SubTreatment))
                                {
                                    try
                                    {
                                        MainDataContext.MainContext.SubTreatments.Remove(x as SubTreatment);
                                    }
                                    catch { }

                                    t.SubTreatment.Remove(x as SubTreatment);
                                }

                                if (!(t.SubTreatment.Count > 0))
                                {
                                    try
                                    {
                                        MainDataContext.MainContext.Treatments.Remove(t);
                                    }
                                    catch { }

                                    VisitTreatments.Remove(t);
                                }
                            });

                        }

                        if (x is Treatment)
                        {
                            if (VisitTreatments.Contains(x as Treatment))
                            {
                                try
                                {
                                    MainDataContext.MainContext.Treatments.Remove(x as Treatment);
                                }
                                catch { }

                                VisitTreatments.Remove(x as Treatment);
                            }
                        }

                        ChangesHasBeenMade = true;
                        ChangesHasBeenSaved = false;

                        CollectionViewSource.GetDefaultView(VisitTreatments).Refresh();
                    });
                return commandToRemoveVisitTreatment;
            }
        }

        public ICommand CommandToSaveChanges
        {
            get
            {
                if (commandToSaveChanges is null)
                    commandToSaveChanges = new ActionCommand(x =>
                    {
                        if (newVisitDateTime)
                        {
                            DateTime newDateTime = new DateTime(NewVisitDate.Year, NewVisitDate.Month, NewVisitDate.Day, NewVisitHour, NewVisitMinute, 0);

                            SelectedVisit.Date = newDateTime;
                        }
                        try
                        {
                            MainDataContext.MainContext.SaveChanges();

                            ChangesHasBeenSaved = true;
                        }
                        catch
                        {
                            MessageBox.Show("Błąd podczas zapisywania zmian.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
                return commandToSaveChanges;
            }
        }

        #endregion

        #region Methods

        private bool NewVisitDateTimeSet()
        {
            if ( NewVisitDate.Day   != SelectedVisit.Date.Day   ||
                 NewVisitDate.Month != SelectedVisit.Date.Month ||
                 NewVisitDate.Year  != SelectedVisit.Date.Year  ||
                 NewVisitHour       != SelectedVisit.Date.Hour  ||
                 NewVisitMinute     != SelectedVisit.Date.Minute )
            {
                return true;
            }
            return false;
        }

        private void tryToAddTreatment(object treatment, ICollection<Treatment> treatments)
        {
            Treatment newT = new Treatment();
            SubTreatment newST = new SubTreatment();
            Sub2Treatment newS2T = new Sub2Treatment();


            if (treatment is Treatment)
            {
                Treatment t = treatments.Where(x => x.Type == (treatment as Treatment).Type).FirstOrDefault();
                if (t is null)
                {
                    newT.Type = (treatment as Treatment).Type;
                    newT.Description = (treatment as Treatment).Description;
                    treatments.Add(newT);
                }
                    
            }

            if (treatment is SubTreatment)
            {
                bool TreatmentFound = false;

               foreach (Treatment t in treatments)
               {
                    if (t.Type == (treatment as SubTreatment).Treatment.Type)
                    {
                        TreatmentFound = true;

                        SubTreatment stt = t.SubTreatment.Where(x => x.Type == (treatment as SubTreatment).Type).FirstOrDefault();

                        if (stt is null)
                        {
                            newST.Type = (treatment as SubTreatment).Type;
                            newST.Description = (treatment as SubTreatment).Description;
                            t.SubTreatment.Add(newST);
                        }
                    }
               }

               if (!TreatmentFound)
               {
                   newT.Type = (treatment as SubTreatment).Treatment.Type;
                   newT.Description = (treatment as SubTreatment).Treatment.Description;

                   newST.Type = (treatment as SubTreatment).Type;
                   newST.Description = (treatment as SubTreatment).Description;

                   newT.SubTreatment.Add(newST);

                   treatments.Add(newT);
               }
            }

            if (treatment is Sub2Treatment)
            {
                bool TreatmentExist = false;
                bool SubTreatmentExist = false;

                Treatment tempT = default(Treatment);

                SubTreatment tempST = default(SubTreatment);

                TreatmentCollection.ToList().ForEach(y =>
                {
                    foreach (SubTreatment sub in y.SubTreatment)
                    {
                        if (sub.Id == (treatment as Sub2Treatment).SubTreatment.Id)
                            tempST = sub;
                    }

                });
                TreatmentCollection.ToList().ForEach(y =>
                {
                    if (y.Id == tempST.Treatment.Id)
                        tempT = y;
                });


                foreach (Treatment treat in treatments)
                {
                    if (treat.Type == tempT.Type)
                    {
                        TreatmentExist = true;
                        foreach (SubTreatment sub in treat.SubTreatment)
                        {
                            if (sub.Type == tempST.Type)
                            {
                                SubTreatmentExist = true;
                                newS2T.Type = (treatment as Sub2Treatment).Type;
                                newS2T.Description = (treatment as Sub2Treatment).Description;
                                sub.Sub2Treatment.Add(newS2T);
                            }
                        }
                        if (!SubTreatmentExist)
                        {
                            newST.Type = (treatment as Sub2Treatment).SubTreatment.Type;
                            newST.Description = (treatment as Sub2Treatment).SubTreatment.Description;

                            newS2T.Type = (treatment as Sub2Treatment).Type;
                            newS2T.Description = (treatment as Sub2Treatment).Description;

                            newST.Sub2Treatment.Add(newS2T);
                            treat.SubTreatment.Add(newST);
                        }
                    }
                }
                if (!TreatmentExist)
                {
                    newT.Type = tempT.Type;
                    newT.Description = tempT.Description;

                    newST.Type = tempST.Type;
                    newST.Description = tempST.Description;

                    newS2T.Type = (treatment as Sub2Treatment).Type;
                    newS2T.Description = (treatment as Sub2Treatment).Description;

                    newST.Sub2Treatment.Add(newS2T);
                    newT.SubTreatment.Add(newST);
                    treatments.Add(newT);
                }
            }

            
        }

        #endregion // Methods
    }
}
