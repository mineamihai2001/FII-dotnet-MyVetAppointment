namespace VetAppointment.API.DTOs.Create
{
    public class CreateAppointmentDto
    {
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
