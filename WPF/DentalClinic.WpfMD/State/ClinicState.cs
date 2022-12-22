using DentalClinic.WpfMD.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalClinic.WpfMD.State
{
    public class ClinicState<T> : IClinicState<T>
    {
        public event Action<T> EntityCreated = default!;

        private readonly List<T> _entities;
        private readonly IDataService<T> _dataService;
        private readonly Lazy<Task> _initializeLazy;

        public IEnumerable<T> Entities => _entities;

        public ClinicState(IDataService<T> dataService)
        {
            _dataService = dataService;
            _initializeLazy = new Lazy<Task>(Initialize);
            _entities = new();
        }

        public async Task Load()
        {
            await _initializeLazy.Value;
        }

        public async Task Create(T entity)
        {
            await _dataService.Create(entity);
            _entities.Add(entity);
            OnEntityCreated(entity);
        }

        private void OnEntityCreated(T entity)
        {
            EntityCreated?.Invoke(entity);
        }

        private async Task Initialize()
        {
            IEnumerable<T> entities = await _dataService.GetAll();

            _entities.Clear();
            _entities.AddRange(entities);
        }
    }
}
