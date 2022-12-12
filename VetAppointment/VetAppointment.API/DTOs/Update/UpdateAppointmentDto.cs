namespace VetAppointment.API.DTOs.Update
{
    public class UpdateAppointmentDto
    {
        public UpdateAppointmentDto(Guid id, string type, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
