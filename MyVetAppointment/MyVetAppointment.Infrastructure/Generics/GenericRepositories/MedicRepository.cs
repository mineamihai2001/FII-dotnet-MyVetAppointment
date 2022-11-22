using VetAppointment.Application;
using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class MedicRepository : Repository<Medic>
    {
        public MedicRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
