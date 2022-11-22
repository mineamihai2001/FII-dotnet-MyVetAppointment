namespace VetAppointment.Domain.Models
{
    public class Clinic
    {
        public Clinic()
        {

        }
        public Guid superUserId { get; private set; }
        public string Address { get; private set; }
        public string PostalCode { get; private set; }
        public string FiscalNumber { get; private set; }
        
        public DateTime createdDTM { get; private set; }
        public DateTime updatedDTM { get; private set; }
    }
}
