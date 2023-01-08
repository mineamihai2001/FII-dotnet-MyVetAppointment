namespace VetAppointment.API.DTOs.Update
{
    public class UpdateClientDto
    {
        public UpdateClientDto(Guid id, string name, string phoneNumber, string emailAddress, string address)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    }
}
