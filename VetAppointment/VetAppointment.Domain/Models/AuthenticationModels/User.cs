using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Domain.Models.AuthenticationModels
{
    public class User
    {
        public User(string username, string password, string role, string emailAdress, string firstName, string lastName, string phone)
        {
            Id = Guid.NewGuid();
            Username=username;
            Password=password;
            Role=role;
            EmailAdress=emailAdress;
            FirstName=firstName;
            LastName=lastName;
            Phone=phone;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAdress { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
