using VetAppointment.Application;
using VetAppointment.Domain.Models;


namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class MedicineRepository : Repository<Medicine>
    {
        public MedicineRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
