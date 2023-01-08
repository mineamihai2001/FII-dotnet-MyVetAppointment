namespace VetAppointment.API.DTOs.Authenticate
{
    public class UserRegisterDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
