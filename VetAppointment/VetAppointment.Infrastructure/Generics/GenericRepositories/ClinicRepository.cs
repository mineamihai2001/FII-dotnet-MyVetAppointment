using VetAppointment.Application;
using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Repositories
{
    public class ClinicRepository : ITemplateRepository<Clinic>, IClinicRepository
    {
        private readonly DatabaseContext context;

        public ClinicRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Clinic clinic) => context.Clinics.Add(clinic);

        public void Delete(Clinic clinic) => context.Clinics.Remove(clinic);

        public Clinic Get(int id) => context.Clinics.Find(id);

        public List<Clinic> GetAll() => context.Clinics.ToList();

        public void Save() => context.Save();
    }
}
