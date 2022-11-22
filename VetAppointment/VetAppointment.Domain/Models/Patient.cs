namespace VetAppointment.Domain.Models
{
    public class Patient
    {
        public Patient(string name, string species, string race, bool gender, decimal weight, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Species = species;
            Race = race;
            Gender = gender;
            Weight = weight;
            BirthDate = birthDate;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Species { get; private set; }
        public string Race { get; private set; }
        public bool Gender { get; private set; }
        public decimal Weight { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid MedicId { get; private set; }

        public void AttachPetToOwner(Client client)
        {
            OwnerId = client.Id;
        }

        public void AttachPatientToMedic(Medic medic)
        {
            MedicId = medic.Id;
        }
    }
}
