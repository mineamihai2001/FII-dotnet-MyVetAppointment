namespace VetAppointment.Domain.Models
{

	public class Bill
	{
		public Bill(DateTime billingDate, string summary, double paymentSum)
		{
			Id = Guid.NewGuid();
			BillingDate = billingDate;
			Summary = summary;
			PaymentSum = paymentSum;
		}

		public Guid Id { get; private set; }
		public DateTime BillingDate { get; private set; }
		public string Summary { get; private set; }
		public double PaymentSum { get; private set; }
		public Guid AppointmentId { get; private set; }
		public Guid ClientId { get; private set; }
        public List<Medicine> Prescription { get; private set; } = new List<Medicine>();

        public void AttachBillToAppointment(Appointment appointment)
		{
			AppointmentId = appointment.Id;
		}

		public void AttachBillToClient(Client client)
		{
			ClientId = client.Id;
		}

	}
}