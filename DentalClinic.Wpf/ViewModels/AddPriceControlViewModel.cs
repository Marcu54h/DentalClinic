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
    public class AddPriceControlViewModel : BaseViewModel
    {
        #region Fields

        private ICommand addPriceCommand;
        private IDisplayControls controlDisplayer;
        private IPerformPriceOperation priceOperation;
        private IDisplayPriceList priceListDisplayer;

        private PriceList priceList;

        #endregion // Fields

        #region Properties

        public bool? DiscountVisibility { get; set; } = false;

        public bool? LowerPriceVisibility { get; set; } = true;

        public bool? UpperPriceVisibility { get; set; } = true;

        public decimal Discount { get; set; } = 0.0M;

        public decimal LowerPrice { get; set; }

        public decimal UpperPrice { get; set; }

        public int Id { get; set; }

        [Required, MinLength(2)]

        public string Name { get; set; }

        public string PriceName { get; set; } = "Nazwa";

        #endregion // Properties

        #region Constructors

        public AddPriceControlViewModel()
        {
            controlDisplayer = App.container.Resolve<MainWindowViewModel>();

            priceOperation = App.container.Resolve<IPerformPriceOperation>();

            priceListDisplayer = App.container.Resolve<IDisplayPriceList>();

            if(!priceListDisplayer.AddSubGroup)
            {
                if (priceListDisplayer.SelectedItem is PriceList || priceListDisplayer.SelectedItem is null)
                {
                    DiscountVisibility = true;
                    LowerPriceVisibility = false;
                    UpperPriceVisibility = false;
                    PriceName = "Nazwa cennika";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }

                if (priceListDisplayer.SelectedItem is Group)
                {
                    DiscountVisibility = false;
                    LowerPriceVisibility = true;
                    UpperPriceVisibility = true;
                    PriceName = "Typ grupy";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }

                if (priceListDisplayer.SelectedItem is SubGroup)
                {
                    DiscountVisibility = false;
                    LowerPriceVisibility = true;
                    UpperPriceVisibility = true;
                    PriceName = "Nazwa";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }

                if (priceListDisplayer.SelectedItem is Sub2Group)
                {
                    DiscountVisibility = false;
                    LowerPriceVisibility = true;
                    UpperPriceVisibility = true;
                    PriceName = "Nazwa";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }
            }
            else
            {
                if (priceListDisplayer.SelectedItem is PriceList)
                {
                    DiscountVisibility = false;
                    LowerPriceVisibility = true;
                    UpperPriceVisibility = true;
                    PriceName = "Typ główny";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }

                if (priceListDisplayer.SelectedItem is Group)
                {
                    DiscountVisibility = false;
                    LowerPriceVisibility = true;
                    UpperPriceVisibility = true;
                    PriceName = "Nazwa";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }

                if (priceListDisplayer.SelectedItem is SubGroup)
                {
                    DiscountVisibility = false;
                    LowerPriceVisibility = true;
                    UpperPriceVisibility = true;
                    PriceName = "Nazwa";
                    NotifyPropertyChanged(nameof(DiscountVisibility));
                    NotifyPropertyChanged(nameof(LowerPriceVisibility));
                    NotifyPropertyChanged(nameof(UpperPriceVisibility));
                    NotifyPropertyChanged(nameof(PriceName));
                }
            }
           
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandToAddPrice
        {
            get
            {
                if (addPriceCommand == null)
                    addPriceCommand = new ActionCommand(x =>
                    {
                    var SelItem = priceListDisplayer.SelectedItem;

                    if (priceListDisplayer.AddSubGroup)
                    {
                        if (SelItem is PriceList)
                        {
                           
                            priceOperation.AddGroupToPriceList(new Group { Type = Name, LowerPrice = LowerPrice, UpperPrice = UpperPrice }, (PriceList)SelItem);
                        }

                        if (SelItem is Group)
                        {
                            priceOperation.AddSubGroupToGroup(new SubGroup { Name = Name, LowerPrice = LowerPrice, UpperPrice = UpperPrice }, (Group)SelItem);
                        }

                        if (SelItem is SubGroup)
                        {
                            priceOperation.AddSub2GroupToSubGroup(new Sub2Group { Name = Name, LowerPrice = LowerPrice, UpperPrice = UpperPrice }, (SubGroup)SelItem);
                        }
                    }
                    else
                    {
                        if (SelItem is PriceList || SelItem is null)
                        {
                            priceOperation.AddPriceList(new PriceList { Name = Name, Discount = Discount });
                        }

                        if (SelItem is Group)
                        {
                               
                            Group group = new Group
                            {
                                Type = Name,
                                LowerPrice = LowerPrice,
                                UpperPrice = UpperPrice
                            };

                            priceOperation.AddGroupToPriceList(group, ((Group)SelItem).PriceList);
                        }

                        if (SelItem is SubGroup)
                        {
                               
                            SubGroup subGroup = new SubGroup
                            {
                                Name = Name,
                                LowerPrice = LowerPrice
                            };

                            priceOperation.AddSubGroupToGroup(subGroup, ((SubGroup)SelItem).Group);
                        }

                        if (SelItem is Sub2Group)
                        {
                            Sub2Group sub2Group = new Sub2Group
                            {
                                Name = Name,
                                LowerPrice = LowerPrice,
                                UpperPrice = UpperPrice
                            };

                            priceOperation.AddSub2GroupToSubGroup(sub2Group, ((Sub2Group)SelItem).SubGroup);
                            }
                        }

                        priceListDisplayer.Refresh();

                        controlDisplayer.DisplayControl(App.container.Resolve<PriceListControl>());
                    });

                return addPriceCommand;
            }
        }

        #endregion // ICommands
    }
}
