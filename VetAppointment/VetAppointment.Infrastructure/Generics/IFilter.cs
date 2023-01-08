namespace VetAppointment.Infrastructure.Generics
{
    public interface IFilter<T>
    {
        T? Filter(IEnumerable<T> queryableBase);
    }
}