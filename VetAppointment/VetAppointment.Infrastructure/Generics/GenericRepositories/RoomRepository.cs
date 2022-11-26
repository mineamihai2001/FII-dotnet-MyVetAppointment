using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class RoomRepository : Repository<Room>
    {
        public RoomRepository(DatabaseContext context) : base(context)
        {
        }
    }
}