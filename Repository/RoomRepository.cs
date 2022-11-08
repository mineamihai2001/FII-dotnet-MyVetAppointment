using MyVetAppointment.Data;
using MyVetAppointment.Entities;
using MyVetAppointment.Entities.Inventory;
using MyVetAppointment.Entities.Users;

namespace MyVetAppointment.Repository
{
    public class RoomRepository
    {
        private readonly VetDataContext context;
        public RoomRepository(VetDataContext context)
        {
            this.context = context;
        }
        public Room? GetById(int id)
        {
            return this.context.Rooms.First(c => c.Id == id);
        }
        public IEnumerable<Room> GetAll()
        {
            return this.context.Rooms.ToList();
        }
        public void Add(Room room)
        {
            this.context.Rooms.Add(room);
        }
        public void Update(Room category)
        {
            this.context.Update(category);
            this.context.SaveChanges();
        }
        public void Delete(int id)
        {
            //this.context.Rooms.Where(room => rom.Id == id).Delete();
        }
    }
}
