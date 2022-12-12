namespace VetAppointment.API.DTOs.Update
{
    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
    }
}
