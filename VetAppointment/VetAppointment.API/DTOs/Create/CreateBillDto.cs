namespace VetAppointment.API.DTOs.Create
{
    public class CreateBillDto
    {
        public DateTime BillingDate { get; set; }
        public string Summary { get; set; }
        public double PaymentSum { get; set; }
    }
}
