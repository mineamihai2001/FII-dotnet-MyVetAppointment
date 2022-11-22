using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateClientDto dto)
        {
            var client = new Client(dto.Name, dto.PhoneNumber, dto.EmailAddress, dto.Address);
            clientRepository.Add(client);
            clientRepository.SaveChanges();
            return Created(nameof(Get), client);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(clientRepository.GetAll());
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
