using VetAppointment.Application;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Repositories;


namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class AppointmentRepository : ITemplateRepository<Appointment>
    {
        private readonly DatabaseContext context;

        public AppointmentRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public void Add(Appointment item) => context.Appointments.Add(item);

        public void Delete(Appointment item) => context.Appointments.Remove(item);

        public Appointment Get(int id) => context.Appointments.Find(id);

        public List<Appointment> GetAll() => context.Appointments.ToList();

        public void Save() => context.Save();
    }
}
