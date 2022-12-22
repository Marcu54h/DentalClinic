using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State;
using DentalClinic.WpfMD.State.Navigator;
using DentalClinic.WpfMD.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace DentalClinic.WpfMD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; } = default!;

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<INavigationStore, NavigationStore>();
                    services.AddViewModelsFactory();
                    services.AddTransient(typeof(IClinicState<>), typeof(ClinicState<>));
                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var navigationStore = AppHost.Services.GetRequiredService<INavigationStore>();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            var vmFactory = AppHost.Services.GetRequiredService<IViewModelsFactory>();
            navigationStore.CurrentView = vmFactory.Create(ViewType.PatientsViewModel);
            startupForm.DataContext = vmFactory.Create(ViewType.MainViewModel);

            startupForm.Show();

            base.OnStartup(e);

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            AppHost.Dispose();
            base.OnExit(e);
        }
    }
}
