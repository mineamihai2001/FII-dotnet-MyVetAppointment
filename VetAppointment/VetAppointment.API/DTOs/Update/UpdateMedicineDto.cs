namespace VetAppointment.API.DTOs.Update
{
    public class UpdateMedicineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PricePerUnit { get; set; }
        public int Stock { get; set; }
    }
}
