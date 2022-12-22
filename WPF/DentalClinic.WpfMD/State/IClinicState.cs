using System;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.State
{
    public interface IClinicState<T>
    {
        event Action<T> EntityCreated;

        Task Create(T entity);
        Task Load();
    }
}