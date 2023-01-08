namespace VetAppointment.Domain.Models
{
    public class Appointment
	{
		public Appointment(string type, DateTime startDate, DateTime endDate, string description)
		{
			Id = Guid.NewGuid();
			Type = type;
			StartDate = startDate;
			EndDate = endDate;
			Description = description;
		}

		public Guid Id { get; private set; }
		public string Type { get; private set; }
		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }
		public string Description { get; private set; }
		public Medic? Medic { get; private set; }
		public Room? Room { get; private set; }
		public Patient? Patient { get; private set; }
		public Client? Client { get; private set; }
		public Bill? Bill { get; private set; }

		public void AttachAppointmentToRoom(Room room)
		{
			Room = room;
		}

		public void AttachAppointmentToMedic(Medic medic)
		{
			Medic = medic;
		}

        public void AttachAppointmentToPatient(Patient patient)
        {
            Patient = patient;
        }

        public void AttachAppointmentToClient(Client client)
		{
			Client = client;
		}

		public void AttachAppointmentToBilling(Bill bill)
		{
			Bill = bill;
		}

	}
}
