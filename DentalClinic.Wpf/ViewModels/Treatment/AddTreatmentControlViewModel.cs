namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Windows;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class AddTreatmentControlViewModel : BaseViewModel
    {
        #region Fields

        private bool addTreatment;

        private bool addSubTreatment;

        private bool addSub2Treatment;

        private ICommand addTreatmentCommand;

        private ICommand commandToGoBack;

        private string topTextBlock;

        #endregion // Fields

        #region Properties

        public string Description { get; set; }

        public string TopTextBlock
        {
            get { return topTextBlock; }
            set { topTextBlock = value; NotifyPropertyChanged(nameof(TopTextBlock)); }
        }

        [Required, MinLength(2)]
        public string Type { get; set; }

        #endregion // Properties

        #region Constructors

        public AddTreatmentControlViewModel()
        {
            if (App.container.Resolve<Container>().SelectedTreatment is null &&
                App.container.Resolve<Container>().SelectedSubTreatment is null &&
                App.container.Resolve<Container>().SelectedSub2Treatment is null)
            {
                addTreatment = true;
                TopTextBlock = "Dodawanie nowego rodzaju zabiegów";
                return;
            }

            if (!(App.container.Resolve<Container>().SelectedTreatment is null))
            {
                addSubTreatment = true;
                TopTextBlock = "Dodawanie nowego podtypu zabiegów";
                return;
            }

            if(!(App.container.Resolve<Container>().SelectedSubTreatment is null))
            {
                addSub2Treatment = true;
                TopTextBlock = "Dodawanie nowego potypu II poziomu";
                return;
            }


        }

        #endregion // Constructors

        #region Methods

        public ICommand CommandToAddTreatment
        {
            get
            {
                if (addTreatmentCommand == null)
                    addTreatmentCommand = new ActionCommand(x =>
                    {
                        if (addTreatment)
                        {
                            try
                            {
                                MainDataContext.MainContext.Treatments.Add(new Treatment
                                {
                                    Type = Type,
                                    Description = Description
                                });

                                MainDataContext.MainContext.SaveChanges();
                            }
                            catch
                            {
                                MessageBox.Show("Coś poszło nie tak podczas dodawania nowego rodzaju zabiegów.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }

                        if (addSubTreatment)
                        {
                            try
                            {
                                MainDataContext.MainContext.SubTreatments.Add(new SubTreatment
                                {
                                    TreatmentId = App.container.Resolve<Container>().SelectedTreatment.Id,
                                    Type = Type,
                                    Description = Description
                                });

                                MainDataContext.MainContext.SaveChanges();
                            }
                            catch
                            {
                                MessageBox.Show("Coś poszło nie tak podczas dodawania nowego podtypu zabiegów.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }

                        if (addSub2Treatment)
                        {
                            try
                            {
                                MainDataContext.MainContext.Sub2Treatments.Add(new Sub2Treatment
                                {
                                    SubTreatmentId = App.container.Resolve<Container>().SelectedSubTreatment.Id,
                                    Type = Type,
                                    Description = Description
                                });

                                MainDataContext.MainContext.SaveChanges();
                            }
                            catch
                            {
                                MessageBox.Show("Coś poszło nie tak podczas dodawania nowego podtypu II poziomu", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                        if (!(App.container.Resolve<Container>().SelectedSub2Treatment is null))
                        {
                            MessageBox.Show("Nie można dodawać podtypu do grup 2 rzędu. No chyba, że jest potrzeba to dzownić do Adama.");
                        }

                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });

                return addTreatmentCommand;
            }
        }

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                    commandToGoBack = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                return commandToGoBack;
            }
        }

        #endregion // Methods
    }
}
