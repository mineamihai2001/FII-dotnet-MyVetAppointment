namespace VetAppointment.API.DTOs.Create
{
    public class CreateRoomDto
    {
        public CreateRoomDto(int roomNumber, string type, int capacity)
        {
            RoomNumber = roomNumber;
            Type = type;
            Capacity = capacity;
        }

        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
    }
}
