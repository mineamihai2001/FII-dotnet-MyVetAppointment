using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Models
{
    public class Medic
    {
        public Medic(string name, string phoneNumber, string emailAddress)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public List<Client> Clients { get; private set; } = new List<Client>();
        public List<Patient> Patients { get; private set; } = new List<Patient>();
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>(); 

       public Result RegisterClientsToMedic(List<Client> clients)
        {
            if (!clients.Any())
            {
                return Result.Failure("Add at least a client to the medic");
            }

            clients.ForEach(client =>
            {
                client.AttachClientToMedic(this);
                Clients.Add(client);
            });
            
            return Result.Success();
        }

        public Result RegisterPatientsToMedic(List<Patient> patients)
        {
            if (!patients.Any())
            {
                return Result.Failure("Add at least a patient to the medic");
            }

            patients.ForEach(patient =>
            {
                patient.AttachPatientToMedic(this);
                Patients.Add(patient);
            });

            return Result.Success();
        }

        public Result RegisterAppointmentsToMedic(List<Appointment> appointments)
        {
            if (!appointments.Any())
            {
                return Result.Failure("Add at least an appointment to the medic");
            }

            appointments.ForEach(appointment =>
            {
                appointment.AttachAppointmentToMedic(this);
                Appointments.Add(appointment);
            });

            return Result.Success();
        }
    }
}
