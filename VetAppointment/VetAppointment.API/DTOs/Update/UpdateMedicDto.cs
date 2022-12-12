namespace VetAppointment.API.DTOs.Update
{
    public class UpdateMedicDto
    {
        public UpdateMedicDto(Guid id, string name, string phoneNumber, string emailAddress)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
