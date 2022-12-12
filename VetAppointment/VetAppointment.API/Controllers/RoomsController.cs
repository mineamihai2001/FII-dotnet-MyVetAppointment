using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;

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
            this.roomRepository = roomRepository;
            this.appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(roomRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateRoomDto dto)
        {
            var room = new Room(dto.Type, dto.RoomNumber, dto.Capacity);
            var validator = new RoomValidator();
            ValidationResult results = validator.Validate(room);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            roomRepository.Add(room);
            roomRepository.SaveChanges();
            return Created(nameof(Get), room);
        }

        [HttpDelete("{roomId:guid}")]
        public IActionResult Delete(Guid roomId)
        {
            var room = roomRepository.GetById(roomId);
            if (room == null) return NotFound();
            roomRepository.Delete(room);
            roomRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateRoomDto dto)
        {
            var room = roomRepository.GetById(dto.Id);
            if (room == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = room.GetType().GetProperty(key).GetValue(room, null);
                if (oldValue != newValue)
                {
                    room.GetType().GetProperty(key).SetValue(room, newValue);
                }
            }
            roomRepository.Update(room);
            roomRepository.SaveChanges();
            return Created(nameof(Get), room);
        }
    }
}
