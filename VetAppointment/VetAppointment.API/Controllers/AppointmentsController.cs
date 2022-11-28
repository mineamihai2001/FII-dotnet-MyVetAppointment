using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs;
using VetAppointment.Domain.Models;
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

        public AppointmentsController(IRepository<Appointment> appointmentRepository, IRepository<Client> clientRepository, 
                                      IRepository<Medic> medicRepository, IRepository<Room> roomRepository, IRepository<Bill> billRespository)
        {
            this.appointmentRepository=appointmentRepository;
            this.clientRepository=clientRepository;
            this.medicRepository=medicRepository;
            this.roomRepository=roomRepository;
            this.billRespository=billRespository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(appointmentRepository.GetAll());
        }

    }
}
