namespace VetAppointment.API.DTOs.Update
{
    public class UpdateBillDto
    {
        public UpdateBillDto(Guid id, DateTime billingDate, string summary, double paymentSum)
        {
            Id = id;
            BillingDate = billingDate;
            Summary = summary;
            PaymentSum = paymentSum;
        }

        public Guid Id { get; set; }
        public DateTime BillingDate { get; set; }
        public string Summary { get; set; }
        public double PaymentSum { get; set; }
    }
}
