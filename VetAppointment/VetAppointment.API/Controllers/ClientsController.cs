using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
//using System.Web.Http;
using VetAppointment.API.DTOs;
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
            clientRepository.Add(client);
            clientRepository.SaveChanges();
            return Created(nameof(Get), client);
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


        [HttpPut("{clientId:guid}")]
        public IActionResult Update(Guid clientId, [FromBody] CreateClientDto dto)
        {
            var client = clientRepository.GetById(clientId);
            if (client == null) return NotFound();
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
