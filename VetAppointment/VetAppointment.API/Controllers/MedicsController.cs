﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
using VetAppointment.Infrastructure.Generics;
using FluentValidation;
using AutoMapper;
using System.Data;
using VetAppointment.Infrastructure.Generics.GenericRepositories;
using Microsoft.AspNetCore.Authorization;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicsController : ControllerBase
    {
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Medic> medicRepository;
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IMapper mapper;

        public MedicsController(IRepository<Client> clientRepository, IRepository<Patient> patientRepository,
            IRepository<Medic> medicRepository, IMapper mapper, IRepository<Appointment> appointmentRepository)
        {
            this.clientRepository = clientRepository;
            this.medicRepository = medicRepository;
            this.patientRepository = patientRepository;
            this.mapper = mapper;
            this.appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await medicRepository.GetAll());
        }

        [HttpGet("{medicId:guid}")]
        public async Task<IActionResult> GetById(Guid medicId)
        {
            return Ok(await medicRepository.GetById(medicId));
        }

        [HttpGet("{medicId:guid}/appointments")]
        //[Authorize(Roles = "Medic")]
        public async Task<IActionResult> GetAppointmenstForMedic(Guid medicId)
        {
            var medic = await medicRepository.GetById(medicId);
            if (medic == null)
            {
                return NotFound("Medic not found!");
            }

            IEnumerable<Appointment> appointments = await appointmentRepository.GetAll();
            return Ok(appointments.ToList().FindAll(a => a.Medic == medic));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMedicDto dto)
        {
            var medic = new Medic(dto.Name, dto.PhoneNumber, dto.EmailAddress);
            var validator = new MedicValidator();
            var results = validator.Validate(medic);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " +
                                      failure.ErrorMessage);
                }

                return BadRequest(results.Errors);
            }

            await medicRepository.Add(medic);
            await medicRepository.SaveChanges();
            return Created(nameof(Get), medic);
        }

        [HttpPost("{medicId:guid}/appointments")]
        //[Authorize(Roles = "Medic")]
        public async Task<IActionResult> RegisterAppointments(Guid medicId,
            [FromBody] List<CreateAppointmentForMedicDto> dtos)
        {
            var medic = await medicRepository.GetById(medicId);
            if (medic == null)
            {
                return NotFound();
            }

            List<Appointment> appointments =
                dtos.Select(d => new Appointment(d.Type, d.StartDate, d.EndDate, d.Description)).ToList();

            medic.RegisterAppointmentsToMedic(appointments);

            appointments.ForEach(a => a.AttachAppointmentToMedic(medic));
            appointments.ForEach(a => appointmentRepository.Add(a));
            await appointmentRepository.SaveChanges();
            await medicRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{medicId:guid}")]
        public async Task<IActionResult> Delete(Guid medicId)
        {
            var medic = await medicRepository.GetById(medicId);
            if (medic == null) return NotFound();
            await medicRepository.Delete(medic);
            await medicRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMedicDto dto)
        {
            var medic = await medicRepository.GetById(dto.Id);
            if (medic == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = medic.GetType().GetProperty(key)!.GetValue(medic, null);
                if (oldValue != newValue)
                {
                    medic.GetType().GetProperty(key)!.SetValue(medic, newValue);
                }
            }

            await medicRepository.Update(medic);
            await medicRepository.SaveChanges();
            return Created(nameof(Get), medic);
        }
    }
}