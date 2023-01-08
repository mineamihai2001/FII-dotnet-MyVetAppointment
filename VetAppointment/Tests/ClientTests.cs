using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetAppointment.Domain.Models;

namespace VetAppointment.Tests
{
    public class ClientTests
    {
        [Fact]
        public void When_RegisterPetsToClient_Then_ShouldReturnSucces()
        {
            //Arrange
            var name = "Dragomir Ionut";
            var phoneNumber = "0765321908";
            var emailAddress = "dragomir.ionut@gmail.com";
            var address = "Iasi";

            var client = new Client(name, phoneNumber, emailAddress, address);
            string dateInput = "Feb 25, 2022";
            var pets = new List<Patient>
            {
                new Patient(
                "Rex",
                "Dog",
                "Bichon",
                true,
                4.5,
                DateTime.Parse(dateInput))
            };

            //Act
            var result = client.RegisterPetsToClient(pets);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterZeroPetsToClient_Then_ShouldReturnFailure()
        {
            //Arrange
            var name = "Dragomir Ionut";
            var phoneNumber = "0765321908";
            var emailAddress = "dragomir.ionut@gmail.com";
            var address = "Iasi";

            var client = new Client(name, phoneNumber, emailAddress, address);
            string dateInput = "Feb 25, 2022";
            var pets = new List<Patient>
            {
                
            };

            //Act
            var result = client.RegisterPetsToClient(pets);

            //Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterBillingsToClient_Then_ShouldReturnSucces()
        {
            //Arrange
            var name = "Dragomir Ionut";
            var phoneNumber = "0765321908";
            var emailAddress = "dragomir.ionut@gmail.com";
            var address = "Iasi";

            var client = new Client(name, phoneNumber, emailAddress, address);
            string dateInput = "Feb 25, 2022";
            var billings = new List<Bill>
            {
                new Bill(
                DateTime.Parse(dateInput),
                "Billing for the appointment",
                250,
                Guid.NewGuid()
                )
            };

            //Act
            var result = client.RegisterBillingsToClient(billings);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void When_RegisterZeroBillingsToClient_Then_ShouldReturnFailure()
        {
            //Arrange
            var name = "Dragomir Ionut";
            var phoneNumber = "0765321908";
            var emailAddress = "dragomir.ionut@gmail.com";
            var address = "Iasi";

            var client = new Client(name, phoneNumber, emailAddress, address);
            var billings = new List<Bill>
            {
               
            };

            //Act
            var result = client.RegisterBillingsToClient(billings);

            //Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
