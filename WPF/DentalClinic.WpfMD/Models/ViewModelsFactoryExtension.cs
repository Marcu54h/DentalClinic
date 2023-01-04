using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DentalClinic.WpfMD.Models
{
    public static class ViewModelsFactoryExtension
    {
        public static void AddViewModelsFactory(this IServiceCollection services)
        {
            services.AddSingleton<IViewType, MainViewModel>();
            services.AddTransient<IViewType, ScheduleViewModel>();
            services.AddTransient<IViewType, PatientsViewModel>();
            services.AddTransient<IViewType, LoginViewModel>();

            services.AddSingleton<Func<IEnumerable<IViewType>>>(x => () => x.GetService<IEnumerable<IViewType>>()!);
            services.AddSingleton<IViewModelsFactory, ViewModelsFactory>();
        }
    }
}
