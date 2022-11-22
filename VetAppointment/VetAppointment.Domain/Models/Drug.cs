namespace VetAppointment.Domain.Models
{
    public class Drug
    {
        public Drug(string name, decimal price, decimal weight, bool prescription)
        {
            Name = name;
            Price = price;
            Weight = weight;
            Prescription = prescription;
        }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public decimal Weight { get; private set; }
        public bool Prescription { get; private set; }
    }
}
