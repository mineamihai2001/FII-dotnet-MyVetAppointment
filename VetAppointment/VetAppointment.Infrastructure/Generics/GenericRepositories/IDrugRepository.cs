using VetAppointment.Domain.Models;

namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public interface IDrugRepository
    {
        void Add(Drug item);
        void Delete(Drug item);
        Drug Get(int id);
        List<Drug> GetAll();
        void Save();
    }
}