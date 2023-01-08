namespace VetAppointment.API.Helpers;

public enum ResponseStatus
{
    Success = 1,
    Error = 0
}

public class Response
{
    public ResponseStatus status { get; private set; }
    public string body { get; private set; }

    public Response(ResponseStatus status, string body)
    {
        this.status = status;
        this.body = body;
    }
}