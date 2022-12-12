namespace VetAppointment.API.DTOs.Update
{
    public class UpdateRoomDto
    {
        public UpdateRoomDto(Guid id, int roomNumber, string type, int capacity)
        {
            Id = id;
            RoomNumber = roomNumber;
            Type = type;
            Capacity = capacity;
        }

        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
    }
}
