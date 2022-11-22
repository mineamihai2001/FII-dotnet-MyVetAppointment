using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class PatientRepository : Repository<Patient>
    {
        public PatientRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
