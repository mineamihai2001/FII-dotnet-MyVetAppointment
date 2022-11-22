using System;

public class Billing
{
	public Billing(DateTime billingDate, string summary, double paymentSum)
	{
        Id = Guid.NewGuid();
		BillingDate = billingDate;
		Summary = summary;
		PaymentSum = paymentSum;
    }

	public Guid id { get; private set; }
	public DateTime BillingDate { get; private set; }
	public string Summary { get; private set; }
	public double PaymentSum { get; private set; }
	public Appointment Appointment { get; private set; }
	public Client Client { get; private set; }

    public void AttachAppointmentToBilling(Appointment appointment)
    {
        Appointment = appointment;
		appointment.AttachBillingToAppointment(appointment);
    }

    public void AttachClientToBilling(Client client)
    {
        Client = client;
    }

}
