using Microsoft.EntityFrameworkCore;
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
        public virtual async Task<T> Add(T entity)
        {
            return context
                .Add(entity)
                .Entity;
        }

        public async Task<T> Delete(T entity)
        {
            return context
                .Remove(entity)
                .Entity;
        }

        public async Task<T> Update(T entity)
        {
            return context
                .Update(entity)
                .Entity;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
