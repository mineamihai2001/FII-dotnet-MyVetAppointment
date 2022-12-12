namespace VetAppointment.API.DTOs.Update
{
    public class UpdateClientDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
                     
        public Guid MedicId { get; set; }
    }
}
