namespace VetAppointment.API.DTOs.Create
{
    public class CreateAppointmentDto
    {
        public CreateAppointmentDto(string type, DateTime startDate, DateTime endDate, string description, Guid medicId, Guid clientId)
        {
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            MedicId = medicId;
            ClientId=clientId;
        }

        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Guid MedicId { get; set; }
        public Guid ClientId { get; set; }
    }
}
