using DentalClinic.Persistence;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State;
using DentalClinic.WpfMD.State.Navigator;
using DentalClinic.WpfMD.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using WebDataSource;
using WebModel;

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
            
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();
#if DEBUG
            hostBuilder.ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            });
#else
            hostBuilder.ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            });
#endif

            hostBuilder.ConfigureServices((hostContext, services) =>
            {
#if DEBUG
                services.AddDbContextFactory<ClinicContext>(options =>
                {
                    options.EnableDetailedErrors();
                    options.EnableThreadSafetyChecks();
                    options.EnableSensitiveDataLogging();
                    options.LogTo(message => Debug.WriteLine(message));
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DentalClinic") ??
                        throw new InvalidOperationException("Connection string 'DentalClinic' not found.")); ;
                });
#else
                services.AddDbContextFactory<ClinicContext>(options =>
                {
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DentalClinic") ??
                        throw new InvalidOperationException("Connection string 'DentalClinic' not found.")); ;
                });
#endif
                services.AddTransient(typeof(IDataService<>), typeof(GenericDataService<>));
                services.AddSingleton<MainWindow>();
                services.AddViewModelsFactory();
                services.AddTransient(typeof(IClinicState<>), typeof(ClinicState<>));
                services.AddSingleton<INavigationStore, NavigationStore>();
            });

            AppHost = hostBuilder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var navigationStore = AppHost.Services.GetRequiredService<INavigationStore>();
            var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
            var vmFactory = AppHost.Services.GetRequiredService<IViewModelsFactory>();
            navigationStore.CurrentView = vmFactory.Create(ViewType.LoginView);
            startupWindow.DataContext = vmFactory.Create(ViewType.MainView);
            // set LoginView as startup view
            ((MainViewModel)startupWindow.DataContext).CurrentView = navigationStore.CurrentView;
            startupWindow.Show();

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
