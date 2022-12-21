using DentalClinic.WpfMD.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataSource;
using WebModel;

namespace DentalClinic.Persistence
{
    internal class GenericDataService<T> : IDataService<T>
        where T : EntityBase
        
    {
        private readonly ClinicContextFactory _clinicContextFactory;

        public GenericDataService(ClinicContextFactory clinicContextFactory)
        {
            _clinicContextFactory = clinicContextFactory;
        }

        public async Task<T> Create(T entity)
        {
            EntityEntry<T>? entry;
            using(ClinicContext context = _clinicContextFactory.CreateDbContext())
            {
                entry = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
            }

            return entry.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            using(ClinicContext context = _clinicContextFactory.CreateDbContext())
            {
                await context.Set<T>().FirstOrDefaultAsync(e => e.Id)
            }
        }

        public async Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(int id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
