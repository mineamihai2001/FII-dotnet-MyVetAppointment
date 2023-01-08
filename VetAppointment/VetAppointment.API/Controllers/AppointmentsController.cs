using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using NETCore.MailKit.Core;
using VetAppointment.API.DTOs;
using VetAppointment.API.DTOs.Create;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Services;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Medic> medicRepository;
        private readonly CustomEmailService emailService;
        private readonly IRepository<Room> roomRepository;
        private readonly IRepository<Bill> billRespository;
        private readonly IMapper mapper;

        public AppointmentsController(IRepository<Appointment> appointmentRepository,
            IRepository<Client> clientRepository,
            IRepository<Medic> medicRepository, IRepository<Room> roomRepository, IRepository<Bill> billRespository,
            IMapper mapper, IConfiguration configuration)
        {
            this.appointmentRepository = appointmentRepository;
            this.clientRepository = clientRepository;
            this.medicRepository = medicRepository;
            this.roomRepository = roomRepository;
            this.billRespository = billRespository;
            this.emailService = new CustomEmailService(configuration);
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await appointmentRepository.GetAll());
        }

        [HttpGet("{appointmentId:guid}")]
        public async Task<IActionResult> GetById(Guid appointmentId)
        {
            return Ok(await appointmentRepository.GetById(appointmentId));
        }

        [HttpGet("medic/{medicId:guid}")]
        public async Task<IActionResult> GetByMedicId(Guid medicId)
        {
            var appointments = appointmentRepository
                .GetAll()
                .Result!
                .Where(a => a.MedicId == medicId).ToList();
            return Ok(appointments);
        }

        [HttpGet("payment/{appointmentId:guid}")]
        public async Task<IActionResult> PayAppointment(Guid appointmentId)
        {
            var appointment = await appointmentRepository.GetById(appointmentId);
            appointment.IsPayed = true;
            await appointmentRepository.Update(appointment);
            await appointmentRepository.SaveChanges();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var medic = await medicRepository.GetById(dto.MedicId);
            if (medic == null)
            {
                return NotFound("Medic not found!");
            }

            var client = await clientRepository.GetById(dto.ClientId);
            if (client == null)
            {
                return NotFound("Client not found!");
            }

            var appointment = new Appointment(dto.Type, dto.StartDate, dto.EndDate, dto.Description);
            appointment.AddMedicToAppointment(medic.Id);
            appointment.AddClientToAppointmet(client.Id);

            // validate the appointment
            var validator = new AppointmentValidator();
            ValidationResult results = validator.Validate(appointment);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " +
                                      failure.ErrorMessage);
                }

                return BadRequest(results.Errors);
            }

            // Add the new appointment
            await appointmentRepository.Add(appointment);
            await appointmentRepository.SaveChanges();

            // SEND EMAIL WITH THE PAYMENT
            string subject = "Appointment Created";
            string body =
                string.Format(
                    "<h1>Hi {0}</h1>. " +
                    "<p>An appointment was created for you." +
                    " Please use the following link for payment.</p> " +
                    "<a target='_blank' href='http://localhost:3000/payment?id={1}'>Pay here</a>",
                    client.Name, appointment.Id);
            emailService.Send(client.EmailAddress, subject, body);

            return Created(nameof(Get), appointment);
        }

        [HttpPost("batch")]
        public IActionResult CreateFromList([FromBody] List<CreateAppointmentDto> dtos)
        {
            List<Appointment> response = new List<Appointment>();
            var validator = new AppointmentValidator();
            dtos.ForEach(async dto =>
            {
                var medic = await medicRepository.GetById(dto.MedicId);
                var client = await clientRepository.GetById(dto.ClientId);
                if (medic == null)
                {
                    Console.WriteLine("The medic " + dto.MedicId + " doesn't exist!");
                }
                else if (client == null)
                {
                    Console.WriteLine("The client " + dto.ClientId + " doesn't exist!");
                }
                else
                {
                    var appointment = new Appointment(dto.Type, dto.StartDate, dto.EndDate, dto.Description);

                    appointment.AttachAppointmentToMedic(medic);
                    medic.RegisterAppointmentsToMedic(new List<Appointment>() { appointment });

                    appointment.AttachAppointmentToClient(client);
                    client.RegisterAppointmentsToClient(new List<Appointment>() { appointment });

                    ValidationResult results = validator.Validate(appointment);
                    if (!results.IsValid)
                    {
                        foreach (var failure in results.Errors)
                        {
                            Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " +
                                              failure.ErrorMessage);
                        }
                    }
                    else
                    {
                        await appointmentRepository.Add(appointment);
                        await appointmentRepository.SaveChanges();
                        await medicRepository.SaveChanges();
                        await clientRepository.SaveChanges();
                        response.Add(appointment);
                    }
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