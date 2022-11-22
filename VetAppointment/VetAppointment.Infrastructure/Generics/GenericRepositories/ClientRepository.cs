using VetAppointment.Application;
using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
