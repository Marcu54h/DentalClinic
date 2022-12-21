using DentalClinic.WpfMD.Models;

namespace DentalClinic.WpfMD.Abstraction
{
    public interface IViewModelsFactory
    {
        IViewType Create(ViewType viewType);
    }
}
