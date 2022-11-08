using Microsoft.EntityFrameworkCore;
using MyVetAppointment.Entities;
using MyVetAppointment.Entities.Inventory;
using MyVetAppointment.Entities.Users;


namespace MyVetAppointment.Data
{
    public class VetDataContext: DbContext

    {
        public VetDataContext()
        {
            //this.Database.EnsureCreated();
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Major> Majors { get; set; }
        
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyVetAppointment.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //List<Appointment> appointments = ; 
            //var room1 = new Room { Id = 1, MainVetId = 1, Appointments = , Drugs = }
            

        }
    }
}
