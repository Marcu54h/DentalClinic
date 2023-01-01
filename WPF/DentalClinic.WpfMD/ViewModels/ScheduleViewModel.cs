using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.State.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebModel;

namespace DentalClinic.WpfMD.ViewModels
{
    public class ScheduleViewModel : ViewModelBase, IViewType
    {
        private IEnumerable<Visit> _visits;

        public ScheduleViewModel(INavigationStore navigationStore)
            : base(navigationStore)
        {
            _visits = Enumerable.Empty<Visit>();

            SeedData();
        }

        public ViewType ViewType => ViewType.ScheduleViewModel;

        public IEnumerable<Visit> Visits { get => _visits; set => SetField(ref _visits, value); }

        private void SeedData()
        {
            Visit visit1 = new() { Id = 1, Date = DateTime.Now };
            Visit visit2 = new() { Id = 2, Date = DateTime.Now };
            Visits = _visits.Append(visit1);
            Visits = _visits.Append(visit2);
        }
    }
}
