using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyVetAppointment.Entities.Inventory;

namespace MyVetAppointment.Entities
{
    public class Room
    {
        public Room(int id, int mainVetId) // List<Appointment> appointments, List<Drug> drugs
        {
            Id = id;
            MainVetId = mainVetId;
            //Appointments = appointments;
            
        }
        public int Id { get; set; }
        public int MainVetId { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Drug> Drugs { get; set; }
    }
}
