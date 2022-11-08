using MyVetAppointment.Data;
using MyVetAppointment.Entities;
using MyVetAppointment.Entities.Inventory;
using MyVetAppointment.Entities.Users;



namespace MyVetAppointment
{
    public class AppointmentRepository
    {
        private readonly VetDataContext context;
        public AppointmentRepository(VetDataContext context)
        {
            this.context = context;
        }
        public Appointment? GetById(int id)
        {
            return this.context.Appointments.First(c => c.Id == id);
        }
        public IEnumerable<Appointment> GetAll()
        {
            return this.context.Appointments.ToList();
        }
        public void Add(Appointment appointment)
        {
            this.context.Appointments.Add(appointment);
        }
        public void Update(Appointment category)
        {
            this.context.Update(category);
            this.context.SaveChanges();
        }
        public void Delete(int id)
        {
            //this.context.Appointments.Where(app => app.Id == id).Delete();
        }
    }
}