namespace VetAppointment.API.DTOs
{
    public class CreatePatientDto
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public bool Gender { get; set; }
        public decimal Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
