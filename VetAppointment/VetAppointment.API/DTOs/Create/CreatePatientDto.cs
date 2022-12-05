namespace VetAppointment.API.DTOs.Create
{
    public class CreatePatientDto
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public bool Gender { get; set; }
        public double Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
