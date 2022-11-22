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

        public DbSet<Clinic> Clinics => Set<Clinic>();

        public DbSet<SuperUser> SuperUsers => Set<SuperUser>();;

        public DbSet<Drug> Drugs => Set<Drug>();

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
