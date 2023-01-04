using CommunityToolkit.Mvvm.ComponentModel;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModel;

namespace DentalClinic.WpfMD.ViewModels
{
    public partial class ScheduleViewModel : ObservableRecipient, IViewType
    {
        [ObservableProperty]
        private IEnumerable<Visit> visits;

        public ScheduleViewModel()
        {
            visits = Enumerable.Empty<Visit>();

            SeedData();
        }

        public ViewType ViewType => ViewType.ScheduleView;


        private void SeedData()
        {
            Visit visit1 = new() { Id = 1, Date = DateTime.Now };
            Visit visit2 = new() { Id = 2, Date = DateTime.Now };
            Visits = visits.Append(visit1);
            Visits = visits.Append(visit2);
        }
    }
}
