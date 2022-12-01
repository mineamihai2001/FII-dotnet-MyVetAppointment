namespace VetAppointment.API.DTOs
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

        public Guid MedicId { get; set; }
    }
}
