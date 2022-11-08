using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVetAppointment.Entities.Users
{
    public class Veterinarian
    {
        public Veterinarian(int id, string firstName, string lastName, int majorId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MajorId = majorId;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int MajorId { get; set; }
    }

}
