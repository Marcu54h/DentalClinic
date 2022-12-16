namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class TeethDiagramViewModel : BaseViewModel
    {
        #region Fields

        private ICommand commandToCheckThisTooth;

        private ICommand commandToClick;

        private ICommand commandToUncheckThisTooth;

        private bool deciduousTeeth;

        private bool permanentTeeth = true;

        private bool tooth11Checked;
        private bool tooth12Checked;
        private bool tooth13Checked;
        private bool tooth14Checked;
        private bool tooth15Checked;
        private bool tooth16Checked;
        private bool tooth17Checked;
        private bool tooth18Checked;

        private bool tooth21Checked;
        private bool tooth22Checked;
        private bool tooth23Checked;
        private bool tooth24Checked;
        private bool tooth25Checked;
        private bool tooth26Checked;
        private bool tooth27Checked;
        private bool tooth28Checked;

        private bool tooth31Checked;
        private bool tooth32Checked;
        private bool tooth33Checked;
        private bool tooth34Checked;
        private bool tooth35Checked;
        private bool tooth36Checked;
        private bool tooth37Checked;
        private bool tooth38Checked;

        private bool tooth41Checked;
        private bool tooth42Checked;
        private bool tooth43Checked;
        private bool tooth44Checked;
        private bool tooth45Checked;
        private bool tooth46Checked;
        private bool tooth47Checked;
        private bool tooth48Checked;

        private int patientImageBorderThickness = 0;

        private string bindingLabel = string.Empty;

        #endregion // Fields

        #region Properties

        public bool DeciduousTeeth
        {
            get { return deciduousTeeth; }
            set
            {
                deciduousTeeth = value;
                setToothSelection();
                NotifyPropertyChanged(nameof(DeciduousTeeth));
            }
        }

        public bool PermanentTeeth
        {
            get { return permanentTeeth; }

            set
            {
                permanentTeeth = value;
                setToothSelection();
                NotifyPropertyChanged(nameof(PermanentTeeth));
            }
        }

        public bool Tooth11Checked { get { return tooth11Checked; } set { tooth11Checked = value; NotifyPropertyChanged(nameof(Tooth11Checked)); } }
        public bool Tooth12Checked { get { return tooth12Checked; } set { tooth12Checked = value; NotifyPropertyChanged(nameof(Tooth12Checked)); } }
        public bool Tooth13Checked { get { return tooth13Checked; } set { tooth13Checked = value; NotifyPropertyChanged(nameof(Tooth13Checked)); } }
        public bool Tooth14Checked { get { return tooth14Checked; } set { tooth14Checked = value; NotifyPropertyChanged(nameof(Tooth14Checked)); } }
        public bool Tooth15Checked { get { return tooth15Checked; } set { tooth15Checked = value; NotifyPropertyChanged(nameof(Tooth15Checked)); } }
        public bool Tooth16Checked { get { return tooth16Checked; } set { tooth16Checked = value; NotifyPropertyChanged(nameof(Tooth16Checked)); } }
        public bool Tooth17Checked { get { return tooth17Checked; } set { tooth17Checked = value; NotifyPropertyChanged(nameof(Tooth17Checked)); } }
        public bool Tooth18Checked { get { return tooth18Checked; } set { tooth18Checked = value; NotifyPropertyChanged(nameof(Tooth18Checked)); } }
        public bool Tooth21Checked { get { return tooth21Checked; } set { tooth21Checked = value; NotifyPropertyChanged(nameof(Tooth21Checked)); } }
        public bool Tooth22Checked { get { return tooth22Checked; } set { tooth22Checked = value; NotifyPropertyChanged(nameof(Tooth22Checked)); } }
        public bool Tooth23Checked { get { return tooth23Checked; } set { tooth23Checked = value; NotifyPropertyChanged(nameof(Tooth23Checked)); } }
        public bool Tooth24Checked { get { return tooth24Checked; } set { tooth24Checked = value; NotifyPropertyChanged(nameof(Tooth24Checked)); } }
        public bool Tooth25Checked { get { return tooth25Checked; } set { tooth25Checked = value; NotifyPropertyChanged(nameof(Tooth25Checked)); } }
        public bool Tooth26Checked { get { return tooth26Checked; } set { tooth26Checked = value; NotifyPropertyChanged(nameof(Tooth26Checked)); } }
        public bool Tooth27Checked { get { return tooth27Checked; } set { tooth27Checked = value; NotifyPropertyChanged(nameof(Tooth27Checked)); } }
        public bool Tooth28Checked { get { return tooth28Checked; } set { tooth28Checked = value; NotifyPropertyChanged(nameof(Tooth28Checked)); } }
        public bool Tooth31Checked { get { return tooth31Checked; } set { tooth31Checked = value; NotifyPropertyChanged(nameof(Tooth31Checked)); } }
        public bool Tooth32Checked { get { return tooth32Checked; } set { tooth32Checked = value; NotifyPropertyChanged(nameof(Tooth32Checked)); } }
        public bool Tooth33Checked { get { return tooth33Checked; } set { tooth33Checked = value; NotifyPropertyChanged(nameof(Tooth33Checked)); } }
        public bool Tooth34Checked { get { return tooth34Checked; } set { tooth34Checked = value; NotifyPropertyChanged(nameof(Tooth34Checked)); } }
        public bool Tooth35Checked { get { return tooth35Checked; } set { tooth35Checked = value; NotifyPropertyChanged(nameof(Tooth35Checked)); } }
        public bool Tooth36Checked { get { return tooth36Checked; } set { tooth36Checked = value; NotifyPropertyChanged(nameof(Tooth36Checked)); } }
        public bool Tooth37Checked { get { return tooth37Checked; } set { tooth37Checked = value; NotifyPropertyChanged(nameof(Tooth37Checked)); } }
        public bool Tooth38Checked { get { return tooth38Checked; } set { tooth38Checked = value; NotifyPropertyChanged(nameof(Tooth38Checked)); } }
        public bool Tooth41Checked { get { return tooth41Checked; } set { tooth41Checked = value; NotifyPropertyChanged(nameof(Tooth41Checked)); } }
        public bool Tooth42Checked { get { return tooth42Checked; } set { tooth42Checked = value; NotifyPropertyChanged(nameof(Tooth42Checked)); } }
        public bool Tooth43Checked { get { return tooth43Checked; } set { tooth43Checked = value; NotifyPropertyChanged(nameof(Tooth43Checked)); } }
        public bool Tooth44Checked { get { return tooth44Checked; } set { tooth44Checked = value; NotifyPropertyChanged(nameof(Tooth44Checked)); } }
        public bool Tooth45Checked { get { return tooth45Checked; } set { tooth45Checked = value; NotifyPropertyChanged(nameof(Tooth45Checked)); } }
        public bool Tooth46Checked { get { return tooth46Checked; } set { tooth46Checked = value; NotifyPropertyChanged(nameof(Tooth46Checked)); } }
        public bool Tooth47Checked { get { return tooth47Checked; } set { tooth47Checked = value; NotifyPropertyChanged(nameof(Tooth47Checked)); } }
        public bool Tooth48Checked { get { return tooth48Checked; } set { tooth48Checked = value; NotifyPropertyChanged(nameof(Tooth48Checked)); } }

        public int PatientImageBorderThickness
        {
            get { return patientImageBorderThickness; }
            set { patientImageBorderThickness = value; NotifyPropertyChanged(nameof(PatientImageBorderThickness)); }
        }


        public Patient Patient
        {
            get { return App.container.Resolve<Container>().SelectedPatient; }
            set { App.container.Resolve<Container>().SelectedPatient = value; }
        }

        public string TreatmentBinding
        {
            get
            {
                bindingLabel = string.Empty;

                if (App.container.Resolve<Container>().CommentPatient)
                {
                    bindingLabel = "PACJENT";
                }

                if (App.container.Resolve<Container>().SelectedTeeth.Count == 1 && !App.container.Resolve<Container>().CommentPatient)
                {
                    Tooth tooth = App.container.Resolve<Container>().SelectedTeeth.First();

                    if (!(tooth is null))
                        bindingLabel = "ZĄB " + tooth.Number;
                }

                if (App.container.Resolve<Container>().SelectedTeeth.Count > 1 && !App.container.Resolve<Container>().CommentPatient)
                {
                    bindingLabel = "ZĘBY ";

                    foreach (Tooth t in App.container.Resolve<Container>().SelectedTeeth.OrderBy(x => x.Number))
                    {
                        bindingLabel += t.Number + " ";
                    }
                }

                return bindingLabel;
            }
        }

        #endregion // Properties

        #region Constructors



        #endregion // Constructors


        #region ICommand

        public ICommand CommandToCheckThisTooth
        {
            get
            {
                if (commandToCheckThisTooth is null)
                    commandToCheckThisTooth = new ActionCommand(x =>
                    {
                        PatientImageBorderThickness = 0;

                        string toothNumber = DeciduousTeeth ? (int.Parse((string)x) + 40).ToString() : (string)x;

                        App.container.Resolve<Container>().CommentPatient = false;

                        App.container.Resolve<Container>().SelectedTeeth.Add(new Tooth { Number = toothNumber });

                        NotifyPropertyChanged("TreatmentBinding");
                    });

                return commandToCheckThisTooth;
            }
        }

        public ICommand CommandToClick
        {
            get
            {
                if (commandToClick is null)
                    commandToClick = new ActionCommand(x =>
                    {
                        App.container.Resolve<Container>().SelectedTeeth.Clear();
                        setToothSelection();
                        PatientImageBorderThickness = 3;
                        if ((string)x == "patient")
                            App.container.Resolve<Container>().CommentPatient = true;

                        NotifyPropertyChanged("TreatmentBinding");
                    });
                return commandToClick;
            }
        }

        public ICommand CommandToUncheckThisTooth
        {
            get
            {
                if (commandToUncheckThisTooth is null)
                    commandToUncheckThisTooth = new ActionCommand(x =>
                    {
                        string toothNumber = DeciduousTeeth ? (int.Parse((string)x) + 40).ToString() : (string)x;

                        Tooth tooth = App.container.Resolve<Container>().SelectedTeeth.Where(y => y.Number == toothNumber).FirstOrDefault();

                        if (!(tooth is null))
                        {
                            App.container.Resolve<Container>().SelectedTeeth.Remove(tooth);
                        }
                        NotifyPropertyChanged("TreatmentBinding");
                    });
                
                return commandToUncheckThisTooth;
            }
        }

        #endregion // ICommand

        #region Methods

        private void setToothSelection(bool selected = false)
        {
            Tooth11Checked = selected;
            Tooth12Checked = selected;
            Tooth13Checked = selected;
            Tooth14Checked = selected;
            Tooth15Checked = selected;
            Tooth16Checked = selected;
            Tooth17Checked = selected;
            Tooth18Checked = selected;
            Tooth21Checked = selected;
            Tooth22Checked = selected;
            Tooth23Checked = selected;
            Tooth24Checked = selected;
            Tooth25Checked = selected;
            Tooth26Checked = selected;
            Tooth27Checked = selected;
            Tooth28Checked = selected;
            Tooth31Checked = selected;
            Tooth32Checked = selected;
            Tooth33Checked = selected;
            Tooth34Checked = selected;
            Tooth35Checked = selected;
            Tooth36Checked = selected;
            Tooth37Checked = selected;
            Tooth38Checked = selected;
            Tooth41Checked = selected;
            Tooth42Checked = selected;
            Tooth43Checked = selected;
            Tooth44Checked = selected;
            Tooth45Checked = selected;
            Tooth46Checked = selected;
            Tooth47Checked = selected;
            Tooth48Checked = selected;

            App.container.Resolve<Container>().SelectedTeeth.Clear();

            NotifyPropertyChanged(nameof(TreatmentBinding));
        }

        #endregion // Methods
    }
}
