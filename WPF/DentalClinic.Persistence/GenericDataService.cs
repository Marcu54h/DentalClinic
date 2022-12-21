using DentalClinic.WpfMD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataSource;

namespace DentalClinic.Persistence
{
    internal class GenericDataService<T> : IDataService<T>
    {
        private readonly ClinicContextFactory clinicContextFactory;
        public GenericDataService(ClinicContextFactory)
        {

        }

        public Task<T> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
