using Microsoft.AspNetCore.Http;
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

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicsController : ControllerBase
    {
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Medic> medicRepository;
        private readonly IMapper mapper;

        public MedicsController(IRepository<Client> clientRepository, IRepository<Patient> patientRepository, IRepository<Medic> medicRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.medicRepository = medicRepository;
            this.patientRepository = patientRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await medicRepository.GetAll());
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
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await medicRepository.Add(medic);
            await medicRepository.SaveChanges();
            return Created(nameof(Get), medic);
        }

        [HttpPost("{medicId:guid}/clients")]
        public async Task<IActionResult> RegisterClients(Guid medicId,
            [FromBody] List<CreateClientDto> dtos)
        {
            var medic = await medicRepository.GetById(medicId);
            if (medic == null)
            {
                return NotFound();
            }

            List<Client> clients = dtos.Select(d => new Client(d.Name, d.PhoneNumber, d.EmailAddress, d.Address, medicId)).ToList();

            medic.RegisterClientsToMedic(clients);

            clients.ForEach(c => c.AttachClientToMedic(medic));
            clients.ForEach(c => clientRepository.Add(c));
            await patientRepository.SaveChanges();
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
