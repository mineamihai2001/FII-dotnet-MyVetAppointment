namespace VetAppointment.Domain.Models
{
    public class Medicine
    {
        public Medicine(string name, double pricePerUnit, int stock)
        {
            Id = Guid.NewGuid();
            Name = name;
            PricePerUnit = pricePerUnit;
            Stock = stock;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double PricePerUnit { get; private set; }
        public int Stock { get; private set; }
    }
}
