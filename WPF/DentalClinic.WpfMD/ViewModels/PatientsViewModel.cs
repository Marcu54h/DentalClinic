﻿using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;

namespace DentalClinic.WpfMD.ViewModels
{
    public class PatientsViewModel : ViewModelBase, IViewType
    {
        public ViewType ViewType => ViewType.PatientsViewModel;
    }
}
