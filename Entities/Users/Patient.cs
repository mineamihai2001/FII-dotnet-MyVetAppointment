using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVetAppointment.Entities.Users
{
    public class Patient
    {
        public Patient(int id, string firstName, string lastName, int petId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PetId = petId;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PetId { get; set; }
    }
}
