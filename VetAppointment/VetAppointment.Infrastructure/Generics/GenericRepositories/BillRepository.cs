using VetAppointment.Application;
using VetAppointment.Domain.Models;


namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class BillRepository : Repository<Bill>
    {
        public BillRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
