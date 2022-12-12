namespace VetAppointment.API.DTOs.Update
{
    public class UpdateBillDto
    {
        public Guid Id { get; set; }
        public DateTime BillingDate { get; set; }
        public string? Summary { get; set; }
        public double PaymentSum { get; set; }
    }
}
