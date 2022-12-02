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

        [HttpDelete("{roomId: guid}")]
        public IActionResult Delete(Guid roomId)
        {
            var room = roomRepository.GetById(roomId);
            if (room == null) return NotFound();
            roomRepository.Delete(room);
            roomRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut("{roomId: guid}")]
        public IActionResult Update(Guid roomId, [FromBody] CreateRoomDto dto)
        {
            var room = roomRepository.GetById(roomId);
            if (room == null) return NotFound();
            roomRepository.Update(room);
            roomRepository.SaveChanges();
            return Created(nameof(Get), room);
        }
    }
}
