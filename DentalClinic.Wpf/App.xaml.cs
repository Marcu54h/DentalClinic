namespace DentalClinic.Wpf
{
    using DentalClinic.Data;
    using DentalClinic.Windows;
    using System.Windows;
    using Unity;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnityContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            container = new UnityContainer();
            //container.RegisterSingleton<MainWindow>();
            //container.RegisterType<MainWindow>("MainWindow", new InjectionConstrctor(container.Resolve<IMainWindow>("MainWindow")));

            //container.RegisterType<ActionCommand>(new InjectionConstructor( ));

            #region Singletons

            container.RegisterSingleton<Container>();

            container.RegisterSingleton<IDisplayControls, MainWindowViewModel>();

            container.RegisterSingleton<IDisplayPriceList, PriceListControlViewModel>();

            container.RegisterSingleton<IGatherPatientInfo, PatientScanner>();

            container.RegisterSingleton<IGatherVisitInfo, VisitScanner>();

            container.RegisterSingleton<ISmsNotify, SmsNotifier>();

            container.RegisterSingleton<ISmsServiceStatus, Container>();

            container.RegisterSingleton<IPatientInfo, PatientScanner>();

            container.RegisterSingleton<IPesel, Pesel>();

            container.RegisterSingleton<IVisitInfo, VisitScanner>();

            container.RegisterSingleton<ICommentOperator, PatientTeethOp>();

            container.RegisterSingleton<IKnowWhoCall, CallManager>();

            container.RegisterSingleton<MainWindow>();

            container.RegisterSingleton<MainWindowViewModel>();

            container.RegisterSingleton<PriceListControlViewModel>();

            container.RegisterSingleton<ScheduleWeekControl>();

            container.RegisterSingleton<ScheduleWeekControlViewModel>();

            container.RegisterSingleton<SettingControl>();

            container.RegisterSingleton<SettingControlViewModel>();

            container.RegisterSingleton<VisitOp>();

            #endregion // Singeltons

            #region Types

            container.RegisterType<AddEmployeeControl>();

            container.RegisterType<AddEmployeeControlViewModel>();

            container.RegisterType<AddPatientControl>();

            container.RegisterType<AddPatientControlViewModel>();

            container.RegisterType<AddVisitControl>();

            container.RegisterType<EmployeeControl>();

            container.RegisterType<EmployeeControlViewModel>();

            container.RegisterType<EmployeeWrapper>();

            container.RegisterType<FilesControl>();

            container.RegisterType<FilesControlViewModel>();

            container.RegisterType<OfficeControlViewModel>();

            container.RegisterType<AddTreatmentControlViewModel>();

            container.RegisterType<AddVisitControlViewModel>();

            container.RegisterType<EditPatientControl>();

            container.RegisterType<EditPatientControlViewModel>();

            container.RegisterType<IOffice, OfficeOp>();

            container.RegisterType<IPerformAddressOperation, AddressOp>();

            container.RegisterType<IPerformEmployeeOperation, EmployeeOp>();

            container.RegisterType<IPerformPatientOperation, PatientOp>();

            container.RegisterType<IPerformPriceOperation, PriceListOp>();

            container.RegisterType<IPerformTreatmentOperation, TreatmentOp>();

            container.RegisterType<IVisit, VisitOp>();

            container.RegisterType<IProvideAddressData, AddressWrapper>();

            container.RegisterType<IScheduleDay, ScheduleDay>();

            container.RegisterType<NewVisitViewControl>();

            container.RegisterType<NewVisitViewControlViewModel>();

            container.RegisterType<OfficeControl>();

            container.RegisterType<OfficeControlViewModel>();

            container.RegisterType<PatientViewControl>();

            container.RegisterType<PatientViewControlViewModel>();

            container.RegisterType<ScheduleDayControl>();

            container.RegisterType<ScheduleDayControlViewModel>();

            container.RegisterType<ScheduleControl>();

            container.RegisterType<ScheduleControlViewModel>();

            container.RegisterType<ScheduleNewPatientControl>();

            container.RegisterType<ScheduleNewPatientControlViewModel>();

            container.RegisterType<TeethDiagramView>();

            container.RegisterType<TreatmentControl>();

            container.RegisterType<TreatmentControlViewModel>();

            container.RegisterType<VisitControl>();

            container.RegisterType<VisitControlViewModel>();

            container.RegisterType<VisitViewControl>();

            container.RegisterType<VisitViewControlViewModel>();

            #endregion // Types

            SettingsMaintainer.LoadSettings();

            string conStr = container.Resolve<Container>().ConnectionString;

            MainDataContext.MainContext = new PDContainer(conStr);
            //MainDataContext.MainContext = new PDContainer();

            MainWindow mainWindow = container.Resolve<MainWindow>();

            mainWindow.Height = container.Resolve<Container>().GuiSettings.AppWindowHeight;

            mainWindow.Width = container.Resolve<Container>().GuiSettings.AppWindowWidth;

            mainWindow.WindowState = container.Resolve<Container>().GuiSettings.AppWindowState;

            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                MainDataContext.MainContext.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Wystąpiły błędy podczas zapisywania głównego kontekstu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            SettingsMaintainer.SaveSettings();

            base.OnExit(e);
        }
    }
}