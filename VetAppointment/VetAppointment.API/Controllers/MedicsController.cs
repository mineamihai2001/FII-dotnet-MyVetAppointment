using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicsController : ControllerBase
    {
        private readonly IRepository<Client> clientRepository;
        private readonly IRepository<Patient> patientRepository;
        private readonly IRepository<Medic> medicRepository;

        public MedicsController(IRepository<Client> clientRepository, IRepository<Patient> patientRepository, IRepository<Medic> medicRepository)
        {
            this.clientRepository = clientRepository;
            this.medicRepository = medicRepository;
            this.patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(medicRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateMedicDto dto)
        {
            var medic = new Medic(dto.Name, dto.PhoneNumber, dto.EmailAddress);
            medicRepository.Add(medic);
            medicRepository.SaveChanges();
            return Created(nameof(Get), medic);
        }

        [HttpPost("{medicId:guid}/clients")]
        public IActionResult RegisterClients(Guid medicId,
            [FromBody] List<CreateClientDto> dtos)
        {
            var medic = medicRepository.GetById(medicId);
            if (medic == null)
            {
                return NotFound();
            }

            List<Client> clients = dtos.Select(d => new Client(d.Name, d.PhoneNumber, d.EmailAddress, d.Address, medicId)).ToList();

            medic.RegisterClientsToMedic(clients);

            clients.ForEach(c => c.AttachClientToMedic(medic));
            clients.ForEach(c => clientRepository.Add(c));
            patientRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{medicId:guid}")]
        public IActionResult Delete(Guid medicId)
        {
            var medic = medicRepository.GetById(medicId);
            if (medic == null) return NotFound();
            medicRepository.Delete(medic);
            medicRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut("{medicId:guid}")]
        public IActionResult Update(Guid medicId, [FromBody] CreateMedicDto dto)
        {
            var medic = medicRepository.GetById(medicId);
            if (medic == null) return NotFound();
            medicRepository.Update(medic);
            medicRepository.SaveChanges();
            return Created(nameof(Get), medic);
        }
    }
}
