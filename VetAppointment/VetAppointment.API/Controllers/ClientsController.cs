using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Medic> medicRepository;

        public ClientsController(IRepository<Client> clientRepository, IRepository<Patient> patientRepository, IRepository<Medic> medicRepository)
        {
            this.clientRepository = clientRepository;
            this.medicRepository = medicRepository;
            this.patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await clientRepository.GetAll());
        }


        [HttpGet("table")]
        public async Task<IActionResult> GetFormatted()
        {
            var clients = await clientRepository.GetAll();
            clients!.ToList().ForEach(client => Console.WriteLine(client));
            var result = clients!.Select(async client =>
            {
                IDictionary<string, object> temp = new ExpandoObject()!;
                foreach (PropertyInfo prop in client.GetType().GetProperties())
                {
                    temp[prop.Name] = prop.GetValue(client, null)!;
                }
                var medic = await medicRepository.GetById(client.MedicId);
                temp["Medic"] = medic!.Name;
                return temp;
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
        {
            var client = new Client(dto.Name, dto.PhoneNumber, dto.EmailAddress, dto.Address, dto.MedicId);
            var validator = new ClientValidator();
            ValidationResult results = validator.Validate(client);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await clientRepository.Add(client);
            await clientRepository.SaveChanges();
            return Created(nameof(Get), client);
        }

        [HttpPost("batch")]
        public IActionResult CreateFromList([FromBody] List<CreateClientDto> dtos)
        {
            List<Client> response = new List<Client>();
            var validator = new ClientValidator();
            dtos.ForEach(dto =>
            {
                var client = new Client(dto.Name, dto.PhoneNumber, dto.EmailAddress, dto.Address, dto.MedicId);
                ValidationResult results = validator.Validate(client);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                    }
                    return BadRequest(results.Errors);
                }
                await clientRepository.Add(client);
            	await clientRepository.SaveChanges();
                response.Add(client);
            });
            return Ok(response.Select(client => Created(nameof(Get), client)));
        }

        [HttpDelete("{clientId:guid}")]
        public async Task<IActionResult> Delete(Guid clientId)
        {
            var client = await clientRepository.GetById(clientId);
            if (client == null) return NotFound();
            await clientRepository.Delete(client);
            await clientRepository.SaveChanges();
            return Ok("deleted");
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClientDto dto)
        {
            var client = await clientRepository.GetById(dto.Id);
            if (client == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = client.GetType().GetProperty(key)!.GetValue(client, null);
                if (oldValue != newValue)
                {
                    client.GetType().GetProperty(key)!.SetValue(client, newValue);
                }
            }
            await clientRepository.Update(client);
            await clientRepository.SaveChanges();
            return Created(nameof(Get), client);
        }

        [HttpPost("{clientId:guid}/pets")]
        public async Task<IActionResult> RegisterPets(Guid clientId,
            [FromBody] List<CreatePatientDto> dtos)
        {
            var client = await clientRepository.GetById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            List<Patient> patients = dtos.Select(d => new Patient(d.Name, d.Species, d.Race, d.Gender, d.Weight, d.BirthDate)).ToList();

            client.RegisterPetsToClient(patients);

            patients.ForEach(p => patientRepository.Add(p));
            await patientRepository.SaveChanges();
            return NoContent();
        }
    }
}
