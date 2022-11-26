namespace VetAppointment.Domain.Models
{
    public class Clinic
    {
        public Clinic(string address)
        {

        }

        public Guid Id { get; private set; }
        public string Address { get; private set; }
        public string PostalCode { get; private set; }
        public string FiscalNumber { get; private set; }
    }
}
