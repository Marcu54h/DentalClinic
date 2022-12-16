
using DentalClinic.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// 
    /// </summary>
    public class TreatmentControlViewModel : BaseViewModel
    {
        #region Fields

        private bool treatmentTypeIsSelected = false;
        
        private ICommand commandToAddTreatment;

        private ICommand commandToAddSubTreatment;

        private ICommand commandToDeleteTreatment;

        private ICommand commandToDoubleClick;

        private ICommand commandToSelectedItemChange;

        #endregion // Fields

        #region Properties

        public bool TreatmentTypeIsSelected
        {
            get { return treatmentTypeIsSelected; }
            set { treatmentTypeIsSelected = value; NotifyPropertyChanged(nameof(TreatmentTypeIsSelected)); }
        }

        public ICollection<Treatment> Treatments
        {
            get { return App.container.Resolve<Container>().Treatments; }

            set { App.container.Resolve<Container>().Treatments = value; }
        }
       
        #endregion // Properties

        #region Constructor

        public TreatmentControlViewModel()
        {
            reloadTreatments();
        }


        #endregion // Constructor

        #region ICommands

        public ICommand CommandToAddSubTreatment
        {
            get
            {
                if (commandToAddSubTreatment == null)
                {
                    commandToAddSubTreatment = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().Call<AddTreatmentControl, TreatmentControl>();
                    });
                }
                return commandToAddSubTreatment;
            }
        }

        public ICommand CommandToAddTreatment
        {
            get
            {
                if (commandToAddTreatment == null)
                {
                    commandToAddTreatment = new ActionCommand(x =>
                    {
                        App.container.Resolve<Container>().SelectedTreatment = null;

                        App.container.Resolve<Container>().SelectedSubTreatment = null;

                        App.container.Resolve<Container>().SelectedSub2Treatment = null;

                        App.container.Resolve<IKnowWhoCall>().Call<AddTreatmentControl, TreatmentControl>();
                    });
                }
                return commandToAddTreatment;
            }
        }

        public ICommand CommandToDeleteTreatment
        {
            get
            {
                
                if (commandToDeleteTreatment == null)
                    commandToDeleteTreatment = new ActionCommand(x =>
                    {
                        MessageBoxResult result = MessageBoxResult.None;

                        if (x is Treatment)
                        {
                            result = MessageBox.Show("Czy napewno usunąć grupę zabiegów wraz z podgrupami?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    MainDataContext.MainContext.Treatments.Remove(x as Treatment);

                                    MainDataContext.MainContext.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Błąd podczas usuwania głównego typu zabiegu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                                }

                            }
                        }
                            

                        if (x is SubTreatment)
                        {
                            result = MessageBox.Show("Czy napewno usunąć podgrupę zabiegów wraz z zabiegami?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                MainDataContext.MainContext.SubTreatments.Remove(x as SubTreatment);

                                MainDataContext.MainContext.SaveChanges();
                            }
                        }
                            

                        if (x is Sub2Treatment)
                        {
                            result = MessageBox.Show("Czy napewno usunąć zabieg?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {

                                MainDataContext.MainContext.Sub2Treatments.Remove(x as Sub2Treatment);

                                MainDataContext.MainContext.SaveChanges();
                            }
                        }

                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        App.container.Resolve<MainWindowViewModel>().DisplayControl(App.container.Resolve<TreatmentControl>());
                    });
                return commandToDeleteTreatment;
            }
        }

        public ICommand CommandToDoubleClick
        {
            get
            {
                if (commandToDoubleClick == null)
                    commandToDoubleClick = new ActionCommand(x =>
                    {
                        // TODO: Something to perform when double clicked...

                    });
                return commandToDoubleClick;
            }
        }

        public ICommand CommandToSelectedItemChange
        {
            get
            {
                if (commandToSelectedItemChange is null)
                    commandToSelectedItemChange = new ActionCommand(x =>
                    {
                        if (x is Treatment)
                        {
                            TreatmentTypeIsSelected = true;

                            App.container.Resolve<Container>().SelectedTreatment = x as Treatment;

                            App.container.Resolve<Container>().SelectedSubTreatment = null;

                            App.container.Resolve<Container>().SelectedSub2Treatment = null;

                            NotifyProperitesChanged(nameof(TreatmentTypeIsSelected));
                        }
                        if (x is SubTreatment)
                        {
                            TreatmentTypeIsSelected = true;

                            App.container.Resolve<Container>().SelectedTreatment = null;

                            App.container.Resolve<Container>().SelectedSubTreatment = x as SubTreatment;

                            App.container.Resolve<Container>().SelectedSub2Treatment = null;

                            NotifyProperitesChanged(nameof(TreatmentTypeIsSelected));
                        }
                        if(x is Sub2Treatment)
                        {
                            TreatmentTypeIsSelected = false;

                            App.container.Resolve<Container>().SelectedTreatment = null;

                            App.container.Resolve<Container>().SelectedSubTreatment = null;

                            App.container.Resolve<Container>().SelectedSub2Treatment = x as Sub2Treatment;

                            NotifyPropertyChanged(nameof(TreatmentTypeIsSelected));
                        }
                    });
                return commandToSelectedItemChange;
            }
        }

        #endregion // ICommands

        #region Methods

        private void reloadTreatments()
        {
            var rawTreatments = MainDataContext.MainContext.Treatments
                                                           .Where(x => x.VisitId == null && x.ToothId == null)
                                                           .OrderBy(y => y.Type)
                                                           .ToArray();

            foreach (var t in rawTreatments)
            {
                MainDataContext.MainContext.SubTreatments
                                           .Where(x => x.TreatmentId == t.Id)
                                           .OrderBy(y => y.Type)
                                           .ToList()
                                           .ForEach(z =>
                                           {

                                               MainDataContext.MainContext.Sub2Treatments
                                                                          .Where(q => q.SubTreatmentId == z.Id)
                                                                          .OrderBy(j => j.Type)
                                                                          .ToList()
                                                                          .ForEach(k =>
                                                                          {
                                                                              z.Sub2Treatment.Add(k);
                                                                          });

                                               t.SubTreatment.Add(z);
                                               
                                               
                                           });
            }

            Treatments = rawTreatments;

            CollectionViewSource.GetDefaultView("Treatments").Refresh();
        }
        #endregion // Methods
    }

}
