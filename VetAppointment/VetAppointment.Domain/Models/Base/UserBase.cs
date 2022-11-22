using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetAppointment.Domain.Models.Base
{
    public class UserBase
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime createdDTM { get; private set; }
        public DateTime updatedDTM { get; private set; }
    }
}
