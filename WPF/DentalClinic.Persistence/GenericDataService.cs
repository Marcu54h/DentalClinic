using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebDataSource;
using WebModel;

namespace DentalClinic.Persistence
{
    public class GenericDataService<T> : IDataService<T>
        where T : EntityBase
        
    {
        private readonly IDbContextFactory<ClinicContext> _clinicContextFactory;

        public GenericDataService(IDbContextFactory<ClinicContext> clinicContextFactory)
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

        public async Task<IEnumerable<T>> GetAll<TProperty>(Expression<Func<T, TProperty>> expression = default!)
        {
            using (ClinicContext context = _clinicContextFactory.CreateDbContext())
            {
                IQueryable<T> query;
                if (expression is not null)
                {
                    query = context.Set<T>()
                                   .Include(expression)
                                   .AsNoTracking();
                }
                else
                {
                    query = context.Set<T>()
                                   .AsNoTracking();
                }
                
                return await query.ToListAsync();
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
