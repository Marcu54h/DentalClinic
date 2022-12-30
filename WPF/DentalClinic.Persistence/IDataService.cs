using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DentalClinic.Persistence
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll<TProperty, T2Property>(
            Expression<Func<T, TProperty>> include = default!,
            Expression<Func<TProperty, T2Property>> thanInclude = default!);
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
