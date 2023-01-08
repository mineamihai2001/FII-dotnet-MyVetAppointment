using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Domain.Models.AuthenticationModels
{
    public class User
    {
        public User(string emailAddress, string password, string role, Guid medicId)
        {
            Id = Guid.NewGuid();
            EmailAddress = emailAddress;
            Password = password;
            Role = role;
            MedicId = medicId;
        }

        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid MedicId { get; set; }
    }
}