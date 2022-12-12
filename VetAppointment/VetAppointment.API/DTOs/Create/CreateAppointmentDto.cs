namespace VetAppointment.API.DTOs.Create
{
    public class CreateAppointmentDto
    {
        public CreateAppointmentDto(string type, DateTime startDate, DateTime endDate, string description)
        {
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
