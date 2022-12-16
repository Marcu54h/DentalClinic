namespace DentalClinic.Wpf
{
    using DentalClinic.Windows;
    using DentalClinic.XmlData;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Unity;

    /// <summary>
    /// 
    /// </summary>
    public class SettingControlViewModel : BaseViewModel
    {
        #region Fields

        private bool restartNeeded = false;

        private ICommand commandToGoBack;

        private ICommand commandToSaveSettings;

        private ICommand commandToSendTestSms;

        #endregion // Fields

        #region Properties

        public bool ThisIsMainHost
        {
            get { return App.container.Resolve<Container>().AppSettings.ThisIsMainHost; }
            set
            {
                App.container.Resolve<Container>().AppSettings.ThisIsMainHost = value;
                NotifyPropertyChanged(nameof(ThisIsMainHost));
            }
        }

        public int StartWorkHour
        {
            get { return App.container.Resolve<Container>().AppSettings.StartWorkHour; }
            set
            {
                App.container.Resolve<Container>().AppSettings.StartWorkHour = value;
                NotifyPropertyChanged(nameof(StartWorkHour));
            }
        }

        public int EndWorkHour
        {
            get { return App.container.Resolve<Container>().AppSettings.EndWorkHour; }
            set
            {
                App.container.Resolve<Container>().AppSettings.EndWorkHour = value;
                NotifyPropertyChanged(nameof(EndWorkHour));
            }
        }

        public int ScheduleInterval
        {
            get { return App.container.Resolve<Container>().AppSettings.ScheduleInterval; }
            set
            {
                App.container.Resolve<Container>().AppSettings.ScheduleInterval = value;
                NotifyPropertyChanged(nameof(ScheduleInterval));
            }
        }

        public int SmsServicePort
        {
            get { return App.container.Resolve<Container>().AppSettings.SMSServicePort; }
            set
            {
                App.container.Resolve<Container>().AppSettings.SMSServicePort = value;
                NotifyPropertyChanged(nameof(SmsPhoneIP));
            }
        }

        public string ClinicAddress
        {
            get { return App.container.Resolve<Container>().AppSettings.AddressOfClinic; }
            set
            {
                App.container.Resolve<Container>().AppSettings.AddressOfClinic = value;
                NotifyPropertyChanged(nameof(ClinicAddress));
            }
        }

        public string ClinicName
        {
            get { return App.container.Resolve<Container>().AppSettings.NameOfClinic; }
            set
            {
                App.container.Resolve<Container>().AppSettings.NameOfClinic = value;
                NotifyPropertyChanged(nameof(ClinicName));
            }
        }

        public string DbServerIp
        {
            get { return App.container.Resolve<Container>().AppSettings.DataBaseIP; }
            set
            {
                App.container.Resolve<Container>().AppSettings.DataBaseIP = value;
                
                NotifyPropertyChanged(nameof(DbServerIp));
            }
        }

        public string MessageContent { get; set; }

        public string ReceiverPhone { get; set; }

        public string SmsPhoneIP
        {
            get { return App.container.Resolve<Container>().AppSettings.SMSServicePhoneIP; }
            set
            {
                App.container.Resolve<Container>().AppSettings.SMSServicePhoneIP = value;
                NotifyPropertyChanged(nameof(SmsPhoneIP));
            }
        }

        

        #endregion // Properties

        #region Constructors

        public SettingControlViewModel()
        {
            
        }

        #endregion // Constructors

        #region ICommands

        public ICommand CommandToGoBack
        {
            get
            {
                if (commandToGoBack is null)
                {
                    commandToGoBack = new ActionCommand(x =>
                    {
                        App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                    });
                }
                return commandToGoBack;
            }
        }

        public ICommand CommandToSaveSettings
        {
            get
            {
                if (commandToSaveSettings is null)
                    commandToSaveSettings = new ActionCommand(x =>
                    {
                        SettingsMaintainer.SaveSettings();
                        MessageBox.Show("Ustawienia zostały zapisane", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);

                        if (restartNeeded)
                        {
                            MessageBox.Show("Do wprowadzenia zmian niezbędny jest restart programu.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Information);
                            Application.Current.Shutdown();

                        }
                        else
                        {
                            App.container.Resolve<IKnowWhoCall>().WhoCalledMe();
                        }
                        
                    });
                return commandToSaveSettings;
            }
        }

        public ICommand CommandToSendTestSms
        {
            get
            {
                if (commandToSendTestSms is null)
                    commandToSendTestSms = new ActionCommand(x =>
                    {
                        if (!string.IsNullOrWhiteSpace(ReceiverPhone))
                        {
                            ISms sms = new Sms
                            {
                                DateTime = DateTime.Now,
                                Content = MessageContent,
                                Identifier = Guid.NewGuid(),
                                PersonId = -100,
                                Phone = ReceiverPhone
                            };

                            new SMSService(App.container.Resolve<Container>().AppSettings.SMSServicePhoneIP,
                                           App.container.Resolve<Container>().AppSettings.SMSServicePort,
                                           "master", "mojakochana32167").SendMessage(sms);
                        }
                        else
                        {
                            MessageBox.Show("Wpisz prawidłowy numer telefonu");
                        }
                        
                    });
                return commandToSendTestSms;
            }
        }

        #endregion // ICommands

        #region Methods

      

        #endregion // Methods
    }
}
