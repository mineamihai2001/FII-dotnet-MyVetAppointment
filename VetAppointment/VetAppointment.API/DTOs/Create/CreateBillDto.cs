namespace VetAppointment.API.DTOs.Create
{
    public class CreateBillDto
    {
        public CreateBillDto(DateTime billingDate, string summary, double paymentSum)
        {
            BillingDate = billingDate;
            Summary = summary;
            PaymentSum = paymentSum;
        }

        public DateTime BillingDate { get; set; }
        public string Summary { get; set; }
        public double PaymentSum { get; set; }
    }
}
