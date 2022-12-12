using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Infrastructure.Generics
{
    public interface IRepository<T>
    {
        Task<T?> Add(T entity);
        Task<T?> Update(T entity);
        Task<T?> Delete(T entity);
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>?> GetAll();
        Task<IEnumerable<T>?> Find(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
