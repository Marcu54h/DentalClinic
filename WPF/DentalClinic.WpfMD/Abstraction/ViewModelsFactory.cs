using DentalClinic.WpfMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DentalClinic.WpfMD.Abstraction
{
    public class ViewModelsFactory : IViewModelsFactory
    {
        private readonly Func<IEnumerable<IViewType>> _factory;

        public ViewModelsFactory(Func<IEnumerable<IViewType>> factory)
        {
            _factory = factory;
        }


        public IViewType Create(ViewType viewType)
        {
            var viewsSet = _factory();
            IViewType outputView = viewsSet.Where(v => v.ViewType == viewType).First();
            return outputView;
        }
    }
}
