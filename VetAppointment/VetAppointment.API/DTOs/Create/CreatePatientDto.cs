namespace VetAppointment.API.DTOs.Create
{
    public class CreatePatientDto
    {
        public CreatePatientDto(string name, string species, string race, bool gender, double weight, DateTime birthDate)
        {
            Name = name;
            Species = species;
            Race = race;
            Gender = gender;
            Weight = weight;
            BirthDate = birthDate;
        }

        public string Name { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public bool Gender { get; set; }
        public double Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
