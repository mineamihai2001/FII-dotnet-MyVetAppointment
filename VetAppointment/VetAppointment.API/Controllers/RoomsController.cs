using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;
using VetAppointment.API.DTOs.Create;
using VetAppointment.API.DTOs.Update;
using VetAppointment.Domain.Models;
using VetAppointment.Domain.Validators;
using VetAppointment.Infrastructure.Generics;
using VetAppointment.Infrastructure.Generics.GenericRepositories;
using FluentValidation;
using AutoMapper;

namespace VetAppointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly IRepository<Room> roomRepository;
        private readonly IRepository<Appointment> appointmentRepository;
        private readonly IMapper mapper;

        public RoomsController(IRepository<Room> roomRepository, IRepository<Appointment> appointmentRepository, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            this.appointmentRepository = appointmentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await roomRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var room = new Room(dto.Type, dto.RoomNumber, dto.Capacity);
            var validator = new RoomValidator();
            var results = validator.Validate(room);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
                return BadRequest(results.Errors);
            }
            await roomRepository.Add(room);
            await roomRepository.SaveChanges();
            return Created(nameof(Get), room);
        }

        [HttpDelete("{roomId:guid}")]
        public async Task<IActionResult> Delete(Guid roomId)
        {
            var room = await roomRepository.GetById(roomId);
            if (room == null) return NotFound();
            await roomRepository.Delete(room);
            await roomRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRoomDto dto)
        {
            var room = await roomRepository.GetById(dto.Id);
            if (room == null) return NotFound();

            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                var key = prop.Name;
                var newValue = prop.GetValue(dto, null);
                var oldValue = room.GetType().GetProperty(key)!.GetValue(room, null);
                if (oldValue != newValue)
                {
                    room.GetType().GetProperty(key)!.SetValue(room, newValue);
                }
            }
            await roomRepository.Update(room);
            await roomRepository.SaveChanges();
            return Created(nameof(Get), room);
        }
    }
}
