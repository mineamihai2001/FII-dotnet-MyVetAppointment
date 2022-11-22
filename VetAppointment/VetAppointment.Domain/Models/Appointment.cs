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
		public Medic Medic { get; private set; }
		public Room Room { get; private set; }
		public Patient Patient { get; private set; }
		public Billing Billing { get; private set; }

		public void AttachRoomToAppointment(Room room)
		{
			Room = room;
		}

		public void AttachMedicToAppointment(Medic medic)
		{
			Medic = medic;
		}

		public void AttachPatientToAppointment(Pacient patient)
		{
			Patient = patient;
		}

		public void AttachBillingToAppointment(Billing billing)
		{
			Billing = billing;
		}

	}
}
