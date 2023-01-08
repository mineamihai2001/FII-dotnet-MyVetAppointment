using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Domain.Models;

namespace VetAppointment.Tests
{
    public class RoomTests
    {
        [Fact]
        public void When_RegisterAppointmentsToRoom_Then_ShouldReturnSucces()
        {
            //Arrange

            var type = "Laboratory";
            var roomNumber = 101;
            var capacity = 10;
            var room = new Room(type, roomNumber, capacity);

            string dateInput = "Feb 25, 2022";
            var appointments = new List<Appointment>
            {
                new Appointment("Consultation",
                DateTime.Parse(dateInput),
                DateTime.Parse(dateInput),
                "Routine Consultation")
            };

            //Act
            var result = room.RegisterAppointmentsToRoom(appointments);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterZeroAppointmentsToRoom_Then_ShouldReturnFailure()
        {
            //Arrange

            var type = "Laboratory";
            var roomNumber = 101;
            var capacity = 10;
            var room = new Room(type, roomNumber, capacity);

            string dateInput = "Feb 25, 2022";
            var appointments = new List<Appointment>
            {
                
            };

            //Act
            var result = room.RegisterAppointmentsToRoom(appointments);

            //Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
