namespace VetAppointment.Domain.Models

public class Room
{
	public Room(string type)
	{
		Id = Guid.NewGuid();
		Type = type;
	}

	public Guid Id { get; private set; }
	public string Type { get; private set; }
    public List<Appointment> Appointments { get; private set; } = new List<Appointments>();

    public Result RegisterAppointmentsToRoom(List<Room> appointments)
    {
        if (!appointments.Any())
        {
            return Result.Failure("Add at least an appointment to the room");
        }

        appointments.ForEach(appointment =>
        {
            appointment.AttachRoomToAppointment(this);
            Appointments.Add(appointment);
        });

        return Result.Success();
    }
}
