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
		public Guid MedicId { get; private set; }
		public Guid RoomId { get; private set; }
		public Guid PatientId { get; private set; }
		public Guid BillId { get; private set; }

		public void AttachAppointmentToRoom(Room room)
		{
			RoomId = room.Id;
		}

		public void AttachAppointmentToMedic(Medic medic)
		{
			MedicId = medic.Id;
		}

		public void AttachAppointmentToPatient(Patient patient)
		{
			PatientId = patient.Id;
		}

		public void AttachAppointmentToBilling(Bill bill)
		{
			BillId = bill.Id;
		}

	}
}
