using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IRepository<Medicine> medicineRepository;
        private readonly IMapper mapper;

        public MedicineController(IRepository<Medicine> medicineRepository, IMapper mapper)
        {
            this.medicineRepository = medicineRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await medicineRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicineDto dto)
        {
            var medicine = new Medicine(dto.Name, dto.PricePerUnit, dto.Stock);
            var validator = new MedicineValidator();
            var results = validator.Validate(medicine);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await medicineRepository.Add(medicine);
            await medicineRepository.SaveChanges();
            return Created(nameof(Get), medicine);
        }

        [HttpDelete("{medicineId:guid}")]
        public async Task<IActionResult> Delete(Guid medicineId)
        {
            var medicine = await medicineRepository.GetById(medicineId);
            if (medicine == null) return NotFound();
            await medicineRepository.Delete(medicine);
            await medicineRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMedicineDto dto)
        {
            var medicine = await medicineRepository.GetById(dto.Id);
            if (medicine == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = medicine.GetType().GetProperty(key)!.GetValue(medicine, null);
                if (oldValue != newValue)
                {
                    medicine.GetType().GetProperty(key)!.SetValue(medicine, newValue);
                }
            }
            await medicineRepository.Update(medicine);
            await medicineRepository.SaveChanges();
            return Created(nameof(Get), medicine);
        }

    }
}
