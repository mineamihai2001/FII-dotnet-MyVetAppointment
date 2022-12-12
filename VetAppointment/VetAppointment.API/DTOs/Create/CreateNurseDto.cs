namespace VetAppointment.API.DTOs.Create
{
    public class CreateNurseDto
    {
        public CreateNurseDto(string name, string phoneNumber, string emailAddress)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
