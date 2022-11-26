namespace VetAppointment.API.DTOs
{
    public class CreateMedicineDto
    {
        public string Name { get; set; }
        public double PricePerUnit { get; set; }
        public int Stock { get; set; }
    }
}
