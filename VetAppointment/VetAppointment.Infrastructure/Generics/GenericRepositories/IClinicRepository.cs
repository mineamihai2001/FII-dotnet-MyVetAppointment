using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Repositories
{
    public interface IClinicRepository
    {
        void Add(Clinic clinic);
        void Delete(Clinic clinic);
        Clinic Get(int id);
        List<Clinic> GetAll();
        void Save();
    }
}