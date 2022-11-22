namespace VetAppointment.Domain.Models
{
    public class Drug
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public decimal Weight { get; private set; }
        public bool Prescription { get; private set; }
    }
}
