using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs.Create;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
using VetAppointment.Infrastructure.Generics;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Medic> medicRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IMapper mapper;

        public PatientsController(IRepository<Appointment> appointmentRepository, IRepository<Client> clientRepository,
                                      IRepository<Medic> medicRepository, IRepository<Patient> patientRepository,
                                      IMapper mapper)
        {
            this.appointmentRepository=appointmentRepository;
            this.clientRepository=clientRepository;
            this.medicRepository=medicRepository;
            this.patientRepository=patientRepository;
            this.mapper=mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(patientRepository.GetAll().Result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            var patient = new Patient(dto.Name, dto.Species, dto.Race, dto.Gender, dto.Weight, dto.BirthDate);
            var validator = new PatientValidator();
            ValidationResult results = validator.Validate(patient);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await patientRepository.Add(patient);
            await patientRepository.SaveChanges();
            return Created(nameof(Get), patient);
        }

        [HttpPost("batch")]
        public IActionResult CreateFromList([FromBody] List<CreatePatientDto> dtos)
        {
            List<Patient> response = new List<Patient>();
            var validator = new PatientValidator();
            dtos.ForEach(async dto =>
            {
                var patient = new Patient(dto.Name, dto.Species, dto.Race, dto.Gender, dto.Weight, dto.BirthDate);
                ValidationResult results = validator.Validate(patient);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                    }
                }
                else
                {
                    await patientRepository.Add(patient);
                    await patientRepository.SaveChanges();
                    response.Add(patient);
                }
            });
            return Ok(response.Select(patient => Created(nameof(Get), patient)));
        }

        [HttpDelete("{patientId:guid}")]
        public async Task<IActionResult> Delete(Guid patientId)
        {
            var patient = await patientRepository.GetById(patientId);
            if (patient == null) return NotFound();
            await patientRepository.Delete(patient);
            await patientRepository.SaveChanges();
            return Ok("deleted");
        }
    }
}
