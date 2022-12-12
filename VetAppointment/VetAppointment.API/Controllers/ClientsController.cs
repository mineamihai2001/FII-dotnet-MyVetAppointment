using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
//using System.Web.Http;
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
        public IActionResult Get()
        {
            return Ok(clientRepository.GetAll());
        }


        [HttpGet("table")]
        public IActionResult GetFormatted()
        {
            var clients = clientRepository.GetAll().ToList();
            clients.ForEach(client => Console.WriteLine(client));
            var result = clients.Select(client =>
            {
                IDictionary<string, object> temp = new ExpandoObject();
                foreach (PropertyInfo prop in client.GetType().GetProperties())
                {
                    temp[prop.Name] = prop.GetValue(client, null);
                }
                temp["Medic"] = medicRepository.GetById(client.MedicId).Name;
                return temp;
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateClientDto dto)
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
            clientRepository.Add(client);
            clientRepository.SaveChanges();
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
                clientRepository.Add(client);
                clientRepository.SaveChanges();
                response.Add(client);
            });
            return Ok(response.Select(client => Created(nameof(Get), client)));
        }

        [HttpDelete("{clientId:guid}")]
        public IActionResult Delete(Guid clientId)
        {
            var client = clientRepository.GetById(clientId);
            if (client == null) return NotFound();
            clientRepository.Delete(client);
            clientRepository.SaveChanges();
            return Ok("deleted");
        }


        [HttpPut]
        public IActionResult Update([FromBody] UpdateClientDto dto)
        {
            var client = clientRepository.GetById(dto.Id);
            if (client == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = client.GetType().GetProperty(key).GetValue(client, null);
                if (oldValue != newValue)
                {
                    client.GetType().GetProperty(key).SetValue(client, newValue);
                }
            }
            clientRepository.Update(client);
            clientRepository.SaveChanges();
            return Created(nameof(Get), client);
        }

        [HttpPost("{clientId:guid}/pets")]
        public IActionResult RegisterPets(Guid clientId,
            [FromBody] List<CreatePatientDto> dtos)
        {
            var client = clientRepository.GetById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            List<Patient> patients = dtos.Select(d => new Patient(d.Name, d.Species, d.Race, d.Gender, d.Weight, d.BirthDate)).ToList();

            client.RegisterPetsToClient(patients);

            patients.ForEach(p => patientRepository.Add(p));
            patientRepository.SaveChanges();
            return NoContent();
        }
    }
}
