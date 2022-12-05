namespace VetAppointment.API.DTOs.Update
{
    public class UpdateMedicDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
