using DentalClinic.WpfMD.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
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
                T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                if (entity is not null) 
                {
                    context.Set<T>().Remove(entity);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<T> Get(int id)
        {
            using (ClinicContext context = _clinicContextFactory.CreateDbContext())
            {
                T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (ClinicContext context = _clinicContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (ClinicContext context = _clinicContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}
