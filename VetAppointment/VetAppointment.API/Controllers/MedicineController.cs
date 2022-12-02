using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs;
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

        [HttpPut("{medicineId:guid}")]
        public ActionResult Update(Guid medicineId, [FromBody] CreateMedicineDto dto)
        {
            var medicine = medicineRepository.GetById(medicineId);
            if (medicine == null) return NotFound();
            medicineRepository.Update(medicine);
            medicineRepository.SaveChanges();
            return Created(nameof(Get), medicine);
        }

    }
}
