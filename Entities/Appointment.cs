using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVetAppointment.Entities
{
    public class Appointment
    {
        public Appointment(int id, int vetId, int patientId, int roomId)
        {
            Id = id;
            VetId = vetId;
            PatientId = patientId;
            RoomId = roomId;
        }

        public int Id { get; set; }
        public int VetId { get; set; }
        public int PatientId { get; set; }
        public int RoomId { get; set; }

    }
}
