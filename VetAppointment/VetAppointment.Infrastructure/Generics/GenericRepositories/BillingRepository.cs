using VetAppointment.Application;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Repositories;


namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class BillingRepository : ITemplateRepository<Billing>
    {
        private readonly DatabaseContext context;

        public BillingRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public void Add(Billing item) => context.Billings.Add(item);

        public void Delete(Billing item) => context.Billings.Remove(item);

        public Billing Get(int id) => context.Billings.Find(id);

        public List<Billing> GetAll() => context.Billings.ToList();

        public void Save() => context.Save();
    }
}
