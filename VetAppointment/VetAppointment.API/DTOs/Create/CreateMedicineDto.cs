namespace VetAppointment.API.DTOs.Create
{
    public class CreateMedicineDto
    {
        public CreateMedicineDto(string name, double pricePerUnit, int stock)
        {
            Name = name;
            PricePerUnit = pricePerUnit;
            Stock = stock;
        }

        public string Name { get; set; }
        public double PricePerUnit { get; set; }
        public int Stock { get; set; }
    }
}
