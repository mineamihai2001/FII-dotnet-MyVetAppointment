using Microsoft.EntityFrameworkCore;
using VetAppointment.Domain.Models;

namespace VetAppointment.Application
{
    public interface IDatabaseContext
    {
        DbSet<Medicine> Medicine { get; }
        DbSet<Client> Clients { get; }
        DbSet<Medic> Medics { get; }
        DbSet<Nurse> Nurses { get; }
        DbSet<Patient> Patients { get; }
        DbSet<Room> Rooms { get; }
        DbSet<Bill> Bills { get; }
        DbSet<Appointment> Appointments { get; }

        void Save();
    }
}
