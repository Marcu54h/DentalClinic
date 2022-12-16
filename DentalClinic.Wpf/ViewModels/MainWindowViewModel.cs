namespace DentalClinic.Wpf
{
    using DentalClinic.Windows;
    using DentalClinic.XmlData;
    using System;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel : BaseViewModel, IDisplayControls
    {
        #region Fields

        protected bool _blok = true;

        private bool btnSlideInVis = Properties.Settings.Default.SlideInButtonVisibility;

        private bool btnSlideOutVis = Properties.Settings.Default.SlideOutButtonVisibility;

        private bool smsOnLostConnectionSend = false;

        private ICommand commandVisitsToShowUp;

        private ICommand commandEmployeesToShowUp;

        private ICommand commandFilesToShowUp;

        private ICommand commandMenuSlideIn;

        private ICommand commandMenuSlideOut;

        private ICommand commandOfficesToShowUp;

        private ICommand commandPriceListsToShowUp;

        private ICommand commandScheduleToShowUp;

        private ICommand commandTreatmentsToShowUp;

        private ICommand commandToShowSettings;

        private RemotePort ConnectionStatus = new RemotePort();

        private SMSService smsService;

        private string windowTitle = "Dental SPA";

        private UserControl lastSeen = null;

        private double slideInMenuWidth = 60;
        private double slideOutMenuWidth = 220;

        #endregion // Fields

        #region Properties

        

        public bool blok { get { return _blok; } set { _blok = value; NotifyPropertyChanged(nameof(blok)); } }

        public bool DbConnectionStatus
        {
            get { return App.container.Resolve<Container>().DBConnectionStatus; }
            set
            {
                if (value == false)
                {
                    //MessageBox.Show("Utracono połączenie z bazą danych, działanie programu nie jest możliwe bez połączenia z bazą danych.", "Utrata łączności", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //Environment.Exit(-1);
                    if (!smsOnLostConnectionSend && SMSServiceStatus == true)
                    {
                        ISms sms = new Sms
                        {
                            Content = "Zerwało połącznie z bazą danych w " +
                                                       App.container.Resolve<Container>().AppSettings.NameOfClinic +
                                                       " przy " + App.container.Resolve<Container>().AppSettings.AddressOfClinic,
                            DateTime = DateTime.Now,
                            Identifier = Guid.NewGuid(),
                            PersonId = -100,
                            Phone = "502644288"
                            
                        };

                        smsService.SendMessage(sms);

                        smsOnLostConnectionSend = true;
                    }
                    
                }
                App.container.Resolve<Container>().DBConnectionStatus = value;
                NotifyProperitesChanged(nameof(DbConnectionStatus));
                //MessageBox.Show("Connection status: " + App.container.Resolve<Container>().DBConnectionStatus.ToString());
            }
        }

        public bool SMSServiceStatus
        {
            get { return App.container.Resolve<Container>().SmsServiceStatus; }
            set
            {
                App.container.Resolve<Container>().SmsServiceStatus = value;

                NotifyProperitesChanged(nameof(SMSServiceStatus));
                //MessageBox.Show("Connection status: " + App.container.Resolve<Container>().SMSServiceStatus.ToString());
            }
        }

        public bool SlideInBtnVisibility
        {
            get { return btnSlideInVis; }
            set
            {
                btnSlideInVis = value;

                NotifyPropertyChanged(nameof(SlideInBtnVisibility));

                Properties.Settings.Default.SlideInButtonVisibility = btnSlideInVis;
            }
        }

        public bool SlideOutBtnVisibility
        {
            get { return btnSlideOutVis; }
            set
            {
                btnSlideOutVis = value;

                NotifyPropertyChanged(nameof(SlideOutBtnVisibility));

                Properties.Settings.Default.SlideOutButtonVisibility = btnSlideOutVis;
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

        public double SlideMenuWidth { get; set; } = Properties.Settings.Default.SlideMenuWidth;

        public double SlideInMenuWidth { get { return slideInMenuWidth; } private set { slideInMenuWidth = value; } }

        public double SlideOutMenuWidth { get { return slideOutMenuWidth; } private set { slideOutMenuWidth = value; } }

        public string WindowTitle
        {
            get
            {
                return windowTitle;
            }
            set
            {
                windowTitle = value;

                NotifyPropertyChanged(nameof(WindowTitle));
            }
        }

        public UserControl CurrentContent { get; set; }

        #endregion // Properties

        #region Constructors

        public MainWindowViewModel()
        {
            // Set main window title.
            WindowTitle = windowTitle;

            // Load login control on startup
            DisplayControl(App.container.Resolve<LoginControl>());

            ConnectionStatus.ConnectionStatusChanged += OnConnectionChanged;

            ConnectionStatus.SmsServiceStatusChanged += OnSmsServiceStausChanged;

            smsService = new SMSService(App.container.Resolve<Container>().AppSettings.SMSServicePhoneIP,
                          App.container.Resolve<Container>().AppSettings.SMSServicePort, "master", "mojakochana32167");

            smsService.SmsServiceMalfunction += OnSmsServiceStausChanged;


            //string connectionString = "data source=192.168.101.139\\SQLEXPRESS;initial catalog=DentalClinic;persist security info=True;user id=dental;password=Sakerhet;MultipleActiveResultSets=True;App=EntityFramework";
            string connectionString = App.container.Resolve<Container>().DbTestConnectionString;

            //string connectionString = ConfigurationManager.ConnectionStrings["PDContainer"].ConnectionString;

            var a = ConnectionStatus.StartSqlCheckWithInterval(connectionString, CancellationToken.None);

            //var a = ConnectionStatus.StartCheckWithInterval("127.0.0.1", 139, CancellationToken.None);

            var sms = ConnectionStatus.StartCheckSmsServiceIPWithInterval
                (App.container.Resolve<Container>().AppSettings.SMSServicePhoneIP,
                 CancellationToken.None);

            Console.Write(App.container.Resolve<Container>().ConnectionString);
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandEmployeesToShowUp
        {
            get
            {
                if (commandEmployeesToShowUp is null)
                    commandEmployeesToShowUp = new ActionCommand(a =>
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<EmployeeControl>());
                    });
                return commandEmployeesToShowUp;
            }
        }

        public ICommand CommandFilesToShowUp
        {
            get
            {
                if (commandFilesToShowUp is null)
                    commandFilesToShowUp = new ActionCommand(v =>
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<FilesControl>());
                    });
                return commandFilesToShowUp;
            }
        }

        public ICommand CommandMenuSlideIn
        {
            get
            {
                if (commandMenuSlideIn is null)
                    commandMenuSlideIn = new ActionCommand(a =>
                    {
                        SlideInBtnVisibility = false;
                        Properties.Settings.Default.SlideInButtonVisibility = false;

                        SlideOutBtnVisibility = true;
                        Properties.Settings.Default.SlideOutButtonVisibility = true;

                        SlideMenuWidth = slideInMenuWidth;
                        NotifyPropertyChanged(nameof(SlideMenuWidth));
                        Properties.Settings.Default.SlideMenuWidth = SlideMenuWidth;
                        Properties.Settings.Default.Save();
                    });
                return commandMenuSlideIn;
            }
        }

        public ICommand CommandMenuSlideOut
        {
            get
            {
                if (commandMenuSlideOut is null)
                    commandMenuSlideOut = new ActionCommand(a =>
                    {
                        SlideInBtnVisibility = true;
                        Properties.Settings.Default.SlideInButtonVisibility = true;

                        SlideOutBtnVisibility = false;
                        Properties.Settings.Default.SlideOutButtonVisibility = false;

                        SlideMenuWidth = slideOutMenuWidth;
                        NotifyPropertyChanged(nameof(SlideMenuWidth));
                        Properties.Settings.Default.SlideMenuWidth = SlideMenuWidth;
                        Properties.Settings.Default.Save();
                    });
                return commandMenuSlideOut;
            }
        }

        public ICommand CommandOfficesToShowUp
        {
            get
            {
                if (commandOfficesToShowUp is null)
                    commandOfficesToShowUp = new ActionCommand(x => 
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<OfficeControl>());
                    });
                return commandOfficesToShowUp;
            }
        }

        public ICommand CommandPriceListsToShowUp
        {
            get
            {
                if (commandPriceListsToShowUp is null)
                    commandPriceListsToShowUp = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<PriceListControl>());
                    });
                return commandPriceListsToShowUp;
            }
        }

        public ICommand CommandScheduleToShowUp
        {
            get
            {
                if (commandScheduleToShowUp is null)
                    commandScheduleToShowUp = new ActionCommand(v =>
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<ScheduleControl>());
                    });
                return commandScheduleToShowUp;
            }
        }

        public ICommand CommandTreatmentsToShowUp
        {
            get
            {
                if (commandTreatmentsToShowUp is null)
                    commandTreatmentsToShowUp = new ActionCommand(v =>
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<TreatmentControl>());
                    });
                return commandTreatmentsToShowUp;
            }
        }

        public ICommand CommandVisitsToShowUp
        {
            get
            {
                if (commandVisitsToShowUp is null)
                    commandVisitsToShowUp = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().ClearQueue();
                        lastSeen = null;
                        DisplayControl(App.container.Resolve<VisitControl>());
                    });
                return commandVisitsToShowUp;
            }
        }

        public ICommand CommandToShowSettings
        {
            get
            {
                if (commandToShowSettings is null)
                    commandToShowSettings = new ActionCommand(x =>
                    {
                        if (lastSeen is null)
                        {
                            lastSeen = CurrentContent;
                            App.container.Resolve<IKnowWhoCall>().Call<SettingControl, ScheduleControl>();
                        }
                        else
                        {
                            DisplayControl(lastSeen);
                            lastSeen = null;
                        }
                    });
                return commandToShowSettings;
            }
        }

        #endregion // ICommands

        #region Methods

        public void DisplayControl(UserControl userControl)
        {
            if (userControl is null)
                userControl = App.container.Resolve<LoginControl>();

            CurrentContent = userControl;
            NotifyPropertyChanged(nameof(CurrentContent));
        }

        private void OnConnectionChanged(Object sender, ConnectionChangedEventArgs e)
        {
            DbConnectionStatus = e.ConnectionStatus;
        }

        private void OnSmsServiceStausChanged(object sender, ConnectionChangedEventArgs e)
        {
            SMSServiceStatus = e.ConnectionStatus;
        }
        #endregion // Methods
    }
}
