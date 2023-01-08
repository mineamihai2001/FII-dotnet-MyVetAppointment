namespace VetAppointment.API.DTOs.Create
{
    public class CreateClientDto
    {
        public CreateClientDto(string name, string phoneNumber, string emailAddress, string address)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
        }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

    }
}
