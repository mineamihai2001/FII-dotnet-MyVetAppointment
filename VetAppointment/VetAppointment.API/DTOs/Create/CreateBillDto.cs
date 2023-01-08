namespace VetAppointment.API.DTOs.Create
{
    public class CreateBillDto
    {
        public CreateBillDto(DateTime billingDate, string summary, double paymentSum, Guid appointmentId)
        {
            BillingDate = billingDate;
            Summary = summary;
            PaymentSum = paymentSum;
            AppointmentId = appointmentId;
        }

        public DateTime BillingDate { get; set; }
        public string Summary { get; set; }
        public double PaymentSum { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
