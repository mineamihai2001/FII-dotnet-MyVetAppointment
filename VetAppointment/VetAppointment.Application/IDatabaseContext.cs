using Microsoft.EntityFrameworkCore;
using VetAppointment.Domain.Models;

namespace VetAppointment.Application
{
    public interface IDatabaseContext
    {
        DbSet<Clinic> Clinics { get; }
        DbSet<SuperUser> SuperUsers { get; }
        DbSet<Drug> Drugs { get; }
        DbSet<Client> Clients { get; }
        DbSet<Medic> Medics { get; }
        DbSet<Nurse> Nurses { get; }
        DbSet<Patient> Patients { get; }


        void Save();
    }
}
