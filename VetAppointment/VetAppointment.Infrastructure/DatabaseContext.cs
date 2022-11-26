using Microsoft.EntityFrameworkCore;
using VetAppointment.Application;
using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Medic> Medics => Set<Medic>();

        public DbSet<Client> Clients => Set<Client>();

        public DbSet<Patient> Patients => Set<Patient>();

        public DbSet<Nurse> Nurses => Set<Nurse>();

        public DbSet<Medicine> Medicine => Set<Medicine>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Bill> Bills => Set<Bill>();

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource = VetAppointment.db");
        }
    }
}
