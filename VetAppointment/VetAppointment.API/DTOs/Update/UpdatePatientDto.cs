namespace VetAppointment.API.DTOs.Update
{
    public class UpdatePatientDto
    {
        public UpdatePatientDto(Guid id, string name, string species, string race, bool gender, double weight, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Species = species;
            Race = race;
            Gender = gender;
            Weight = weight;
            BirthDate = birthDate;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public bool Gender { get; set; }
        public double Weight { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
