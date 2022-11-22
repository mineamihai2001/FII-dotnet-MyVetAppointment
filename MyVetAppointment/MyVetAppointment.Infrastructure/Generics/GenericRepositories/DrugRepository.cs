using VetAppointment.Application;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Repositories;


namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class DrugRepository : ITemplateRepository<Drug>
    {
        private readonly DatabaseContext context;

        public DrugRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public void Add(Drug item) => context.Drugs.Add(item);

        public void Delete(Drug item) => context.Drugs.Remove(item);

        public Drug Get(int id) => context.Drugs.Find(id);

        public List<Drug> GetAll() => context.Drugs.ToList();

        public void Save() => context.Save();
    }
}
