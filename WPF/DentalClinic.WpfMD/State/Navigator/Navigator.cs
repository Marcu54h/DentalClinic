using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.State.Navigator
{
    public class Navigator : INavigator
    {
        private readonly IViewModelsFactory _viewModelsFactory;
        private IViewType _viewType = default!;

        public Navigator(IViewModelsFactory viewModelsFactory)
        {
            _viewModelsFactory = viewModelsFactory;
        }

        public IViewType CurrentViewModel
        {
            get { return _viewType; }
            set { _viewType = value; }
        }

        public IAsyncCommand<ViewType> ChangeCurrentView =>
            new AsyncCommand<ViewType>(async (vt) =>
                await Task.Run(() => CurrentViewModel = _viewModelsFactory.Create(vt)));
        
    }
}
