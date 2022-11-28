using Microsoft.AspNetCore.Mvc;
using VetAppointment.API.DTOs;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly IRepository<Room> roomRepository;
        private readonly IRepository<Appointment> appointmentRepository;

        public RoomsController(IRepository<Room> roomRepository, IRepository<Appointment> appointmentRepository)
        {
            this.roomRepository=roomRepository;
            this.appointmentRepository=appointmentRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(roomRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateRoomDto dto)
        {
            var room = new Room( dto.Type, dto.RoomNumber, dto.Capacity);
            roomRepository.Add(room);
            roomRepository.SaveChanges();
            return Created(nameof(Get), room);
        }
    }
}
