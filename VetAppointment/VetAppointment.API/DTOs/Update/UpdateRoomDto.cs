namespace VetAppointment.API.DTOs.Update
{
    public class UpdateRoomDto
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
        public string? Type { get; set; }
        public int Capacity { get; set; }
    }
}
