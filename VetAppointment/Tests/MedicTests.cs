using VetAppointment.Domain.Models;

namespace Tests
{
    public class MedicTests
    {
        [Fact]
        public void When_RegisterClientsToMedic_Then_ShouldReturnSucces()
        {
            //Arrange
            var name = "Iordache Alexandru";
            var phoneNumber = "1234567890";
            var emailAddress = "iordache@gmail.com";
            var medic = new Medic(name, phoneNumber, emailAddress);
            var clients = new List<Client>
            {
                new Client(
                "Dragomir Ionut",
                "0765321908",
                "dragomir.ionut@gmail.com",
                "Iasi",
                medic.Id)
            };
            
            //Act
            var result = medic.RegisterClientsToMedic(clients);
            
            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterZeroClientsToMedic_Then_ShouldReturnFailure()
        {
            //Arrange
            var name = "Iordache Alexandru";
            var phoneNumber = "1234567890";
            var emailAddress = "iordache@gmail.com";
            var medic = new Medic(name, phoneNumber, emailAddress);
            var clients = new List<Client>
            {
                
            };

            //Act
            var result = medic.RegisterClientsToMedic(clients);

            //Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterAppointmentsToMedic_Then_ShouldReturnSucces()
        {
            //Arrange
            var name = "Iordache Alexandru";
            var phoneNumber = "1234567890";
            var emailAddress = "iordache@gmail.com";
            var medic = new Medic(name, phoneNumber, emailAddress);
            
            string dateInput = "Feb 25, 2022";
            var appointments = new List<Appointment>
            {
                new Appointment("Consultation",
                DateTime.Parse(dateInput),
                DateTime.Parse(dateInput),
                "Routine Consultation")
            };

            //Act
            var result = medic.RegisterAppointmentsToMedic(appointments);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterZeroAppointmentsToMedic_Then_ShouldReturnFailure()
        {
            //Arrange
            var name = "Iordache Alexandru";
            var phoneNumber = "1234567890";
            var emailAddress = "iordache@gmail.com";
            var medic = new Medic(name, phoneNumber, emailAddress);

            string dateInput = "Feb 25, 2022";
            var appointments = new List<Appointment>
            {
               
            };

            //Act
            var result = medic.RegisterAppointmentsToMedic(appointments);

            //Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}