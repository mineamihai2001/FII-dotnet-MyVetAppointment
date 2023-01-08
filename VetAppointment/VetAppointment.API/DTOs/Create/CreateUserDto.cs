namespace VetAppointment.API.DTOs.Create;

public class CreateUserDto
{
    public CreateUserDto(string emailAddress, string password, string role, Guid medicId)
    {
        EmailAddress = emailAddress;
        Password = password;
        Role = role;
        MedicId = medicId;
    }

    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public Guid MedicId { get; set; }
}