using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs;
using VetAppointment.API.DTOs.Create;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
using VetAppointment.Infrastructure.Generics;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Medic> medicRepository;
        private readonly IRepository<Room> roomRepository;
        private readonly IRepository<Bill> billRespository;
        private readonly IMapper mapper;

        public AppointmentsController(IRepository<Appointment> appointmentRepository, IRepository<Client> clientRepository, 
                                      IRepository<Medic> medicRepository, IRepository<Room> roomRepository, IRepository<Bill> billRespository,
                                      IMapper mapper)
        {
            this.appointmentRepository=appointmentRepository;
            this.clientRepository=clientRepository;
            this.medicRepository=medicRepository;
            this.roomRepository=roomRepository;
            this.billRespository=billRespository;
            this.mapper=mapper;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(appointmentRepository.GetAll().Result);
        }

        [HttpGet("{appointmentId:guid}")]
        public async Task<IActionResult> GetById(Guid appointmentId)
        {
            return Ok(await appointmentRepository.GetById(appointmentId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var appointment = new Appointment(dto.Type, dto.StartDate, dto.EndDate, dto.Description);
            var validator = new AppointmentValidator();
            ValidationResult results = validator.Validate(appointment);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await appointmentRepository.Add(appointment);
            await appointmentRepository.SaveChanges();
            return Created(nameof(Get), appointment);
        }

        [HttpPost("batch")]
        public IActionResult CreateFromList([FromBody] List<CreateAppointmentDto> dtos)
        {
            List<Appointment> response = new List<Appointment>();
            var validator = new AppointmentValidator();
            dtos.ForEach(async dto =>
            {
                var appointment = new Appointment(dto.Type, dto.StartDate, dto.EndDate, dto.Description);
                ValidationResult results = validator.Validate(appointment);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                    }
                }
                else
                {
                    await appointmentRepository.Add(appointment);
                    await appointmentRepository.SaveChanges();
                    response.Add(appointment);
                }
            });
            return Ok(response.Select(appointment => Created(nameof(Get), appointment)));
        }

        [HttpDelete("{appointmentId:guid}")]
        public async Task<IActionResult> Delete(Guid appointmentId)
        {
            var appointment = await appointmentRepository.GetById(appointmentId);
            if (appointment == null) return NotFound();
            await appointmentRepository.Delete(appointment);
            await appointmentRepository.SaveChanges();
            return Ok("deleted");
        }

    }
}
