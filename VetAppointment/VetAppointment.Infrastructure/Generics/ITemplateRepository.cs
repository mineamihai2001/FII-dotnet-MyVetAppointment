using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Infrastructure.Repositories
{
    public interface ITemplateRepository<T>
    {
        public void Add(T item);
        public List<T> GetAll();
        public T Get(int id);
        public void Delete(T item);
        public void Save();
    }
}
