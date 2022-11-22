using VetAppointment.Application;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Repositories;

namespace VetRoom.Infrastructure.Generics.GenericRepositories
{
    public class RoomRepository : ITemplateRepository<Room>
    {
        private readonly DatabaseContext context;

        public RoomRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public void Add(Room item) => context.Rooms.Add(item);

        public void Delete(Room item) => context.Rooms.Remove(item);

        public Room Get(int id) => context.Rooms.Find(id);

        public List<Room> GetAll() => context.Rooms.ToList();

        public void Save() => context.Save();
    }
}