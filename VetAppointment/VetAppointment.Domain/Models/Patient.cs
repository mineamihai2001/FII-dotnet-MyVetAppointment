using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Models
{
    public class Patient
    {
        public Patient(string name, string species, string race, bool gender, double weight, DateTime birthDate)
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
        public double Weight { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid MedicId { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();

        public void AttachPetToOwner(Client client)
        {
            OwnerId = client.Id;
        }

        public void AttachPatientToMedic(Medic medic)
        {
            MedicId = medic.Id;
        }

        public Result RegisterAppointmentsToPatient(List<Appointment> appointments)
        {
            if (!appointments.Any())
            {
                return Result.Failure("Add at least an appointment to the medic");
            }

            appointments.ForEach(appointment =>
            {
                appointment.AttachAppointmentToPatient(this);
                Appointments.Add(appointment);
            });

            return Result.Success();
        }
    }
}
