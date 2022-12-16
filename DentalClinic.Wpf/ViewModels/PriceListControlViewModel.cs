
using DentalClinic.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// 
    /// </summary>
    public class PriceListControlViewModel : ObservableCollection<PriceList>, IDisplayPriceList
    {
        #region Fields

        private ICommand commandToAddPrice;

        private ICommand commandToAddSubGroup;

        private ICommand commandToDeletePrice;

        private ICommand commandToDoubleClick;

        private ICommand commandToSelectedItemChange;

        private IPerformPriceOperation priceOperation;

        #endregion // Fields

        #region Properties

        [Dependency]
        internal IDisplayControls ControlDisplayer { get; set; }

        public bool PriceGroupIsSelected { get; set; } = false;

        public object SelectedItem { get; set; }

        public bool AddSubGroup { get; set; } = false;

        #endregion // Properties

        #region Constructor

        public PriceListControlViewModel()
        {
            priceOperation = App.container.Resolve<IPerformPriceOperation>();

            Refresh();
        }

        #endregion // Constructor

        #region ICommands

        public ICommand CommandToAddSubGroup
        {
            get
            {
                if (commandToAddSubGroup == null)
                {
                    commandToAddSubGroup = new ActionCommand(x =>
                    {
                        AddSubGroup = true;
                        ControlDisplayer.DisplayControl(App.container.Resolve<AddPriceControl>());
                    });
                }
                return commandToAddSubGroup;
            }
        }

        public ICommand CommandToAddPriceList
        {
            get
            {
                if (commandToAddPrice == null)
                {
                    commandToAddPrice = new ActionCommand(x =>
                    {
                        AddSubGroup = false;
                        ControlDisplayer.DisplayControl(App.container.Resolve<AddPriceControl>());
                    });
                }
                return commandToAddPrice;
            }
        }

        public ICommand CommandToDeletePrice
        {
            get
            {
                MessageBoxResult result = MessageBoxResult.None;
                if (commandToDeletePrice == null)
                    commandToDeletePrice = new ActionCommand(x =>
                    {
                        if (SelectedItem is PriceList)
                        {
                            result = MessageBox.Show("Czy napewno usunąć grupę zabiegów wraz z podgrupami?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                                priceOperation.Delete((PriceList)SelectedItem);
                        }


                        if (SelectedItem is Group)
                        {
                            result = MessageBox.Show("Czy napewno usunąć podgrupę zabiegów wraz z zabiegami?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                                priceOperation.Delete((Group)SelectedItem);
                        }


                        if (SelectedItem is SubGroup)
                        {
                            result = MessageBox.Show("Czy napewno usunąć zabieg?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                                priceOperation.Delete((SubGroup)SelectedItem);
                        }

                        if (SelectedItem is Sub2Group)
                        {
                            result = MessageBox.Show("Czy napewno usunąć zabieg?", "Pytanie...", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                                priceOperation.Delete((Sub2Group)SelectedItem);
                        }


                        Refresh();
                    });
                return commandToDeletePrice;
            }
        }

        public ICommand CommandToDoubleClick
        {
            get
            {
                if (commandToDoubleClick == null)
                    commandToDoubleClick = new ActionCommand(x =>
                    {
                        // TODO: Something to performe when double clicked...

                    });
                return commandToDoubleClick;
            }
        }

        public ICommand CommandToSelectedItemChange
        {
            get
            {
                if (commandToSelectedItemChange == null)
                    commandToSelectedItemChange = new ActionCommand(args => SelectedItemChanged(args));
                return commandToSelectedItemChange;
            }
        }

        #endregion // ICommands

        #region Methods

        public void AddItem(PriceList priceList)
        {
            Add(priceList);
        }

        public void Refresh()
        {
            Items.Clear();
            foreach (PriceList pd in priceOperation.PriceLists)
            {
                AddItem(pd);
            }
            CollectionViewSource.GetDefaultView(this).Refresh();
        }

        private void SelectedItemChanged(object args)
        {
            if (args is PriceList || args is Group || args is SubGroup)
            {
                PriceGroupIsSelected = true;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(PriceGroupIsSelected)));
            }
            else
            {
                PriceGroupIsSelected = false;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(PriceGroupIsSelected)));
            }
            SelectedItem = args;
        }

        #endregion // Methods
    }

}
