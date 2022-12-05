namespace VetAppointment.API.DTOs.Create
{
    public class CreateRoomDto
    {
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
    }
}
