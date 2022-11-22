using System.Linq.Expressions;
using VetAppointment.Application;

namespace VetAppointment.Infrastructure.Generics
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext context;
        public Repository(DatabaseContext context)
        {
            this.context = context;
        }
        public virtual T Add(T entity)
        {
            return context
                .Add(entity)
                .Entity;
        }

        public T Delete(T entity)
        {
            return context
                .Remove(entity)
                .Entity;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>()
                .ToList();
        }

        public T GetById(Guid id)
        {
            return context.Find<T>(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public T Update(T entity)
        {
            return context
                .Update(entity)
                .Entity;
        }
    }
}
