namespace VetAppointment.API.DTOs.Update
{
    public class UpdatePatientDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public string? Race { get; set; }
        public bool Gender { get; set; }
        public double Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
