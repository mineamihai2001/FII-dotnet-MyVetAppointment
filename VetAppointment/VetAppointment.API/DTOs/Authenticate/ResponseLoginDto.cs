using VetAppointment.API.Helpers;
using VetAppointment.Domain.Models.AuthenticationModels;

namespace VetAppointment.API.DTOs.Authenticate;

public class ResponseLoginDto
{
    public ResponseStatus status { get; private set; }
    public string token { get; private set; }
    public Guid Id { get; private set; }
    public Guid medicId { get; private set; }

    public ResponseLoginDto(ResponseStatus status, string token, Guid id, Guid medicId)
    {
        this.status = status;
        this.token = token;
        this.medicId = medicId;
        Id = id;
    }
}