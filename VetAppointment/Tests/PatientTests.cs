using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Domain.Models;

namespace VetAppointment.Tests
{
    public class PatientTests
    {
        [Fact]
        public void When_RegisterAppointmentsToPatient_Then_ShouldReturnSucces()
        {
            //Arrange
            var name = "Rex";
            var species = "Dog";
            var race = "Bichon";
            var gender = true;
            var weight = 4.5;
            var birthDay =  DateTime.Parse("Feb 20, 2019");
            var patient = new Patient(name, species, race, gender, weight, birthDay);

            string dateInput = "Feb 25, 2022";
            var appointments = new List<Appointment>
            {
                new Appointment("Consultation",
                DateTime.Parse(dateInput),
                DateTime.Parse(dateInput),
                "Routine Consultation")
            };

            //Act
            var result = patient.RegisterAppointmentsToPatient(appointments);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterZeroAppointmentsToPatient_Then_ShouldReturnFailure()
        {
            //Arrange
            var name = "Rex";
            var species = "Dog";
            var race = "Bichon";
            var gender = true;
            var weight = 4.5;
            var birthDay = DateTime.Parse("Feb 20, 2019");
            var patient = new Patient(name, species, race, gender, weight, birthDay);

            var appointments = new List<Appointment>
            {
                
            };

            //Act
            var result = patient.RegisterAppointmentsToPatient(appointments);

            //Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
