namespace VetAppointment.API.DTOs.Update
{
    public class UpdateNurseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
