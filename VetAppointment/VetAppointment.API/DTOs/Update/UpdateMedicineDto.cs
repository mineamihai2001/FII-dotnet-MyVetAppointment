namespace VetAppointment.API.DTOs.Update
{
    public class UpdateMedicineDto
    {
        public UpdateMedicineDto(Guid id, string name, double pricePerUnit, int stock)
        {
            Id = id;
            Name = name;
            PricePerUnit = pricePerUnit;
            Stock = stock;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PricePerUnit { get; set; }
        public int Stock { get; set; }
    }
}
