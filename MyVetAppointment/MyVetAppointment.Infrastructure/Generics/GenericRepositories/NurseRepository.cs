using VetAppointment.Application;
using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class NurseRepository : Repository<Nurse>
    {
        public NurseRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
