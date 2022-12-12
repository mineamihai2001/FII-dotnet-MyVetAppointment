using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IRepository<Medicine> medicineRepository;

        public MedicineController(IRepository<Medicine> medicineRepository)
        {
            this.medicineRepository = medicineRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(medicineRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateMedicineDto dto)
        {
            var medicine = new Medicine(dto.Name, dto.PricePerUnit, dto.Stock);
            var validator = new MedicineValidator();
            ValidationResult results = validator.Validate(medicine);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            medicineRepository.Add(medicine);
            medicineRepository.SaveChanges();
            return Created(nameof(Get), medicine);
        }

        [HttpDelete("{medicineId:guid}")]
        public IActionResult Delete(Guid medicineId)
        {
            var medicine = medicineRepository.GetById(medicineId);
            if (medicine == null) return NotFound();
            medicineRepository.Delete(medicine);
            medicineRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateMedicineDto dto)
        {
            var medicine = medicineRepository.GetById(dto.Id);
            if (medicine == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = medicine.GetType().GetProperty(key).GetValue(medicine, null);
                if (oldValue != newValue)
                {
                    medicine.GetType().GetProperty(key).SetValue(medicine, newValue);
                }
            }
            medicineRepository.Update(medicine);
            medicineRepository.SaveChanges();
            return Created(nameof(Get), medicine);
        }

    }
}
