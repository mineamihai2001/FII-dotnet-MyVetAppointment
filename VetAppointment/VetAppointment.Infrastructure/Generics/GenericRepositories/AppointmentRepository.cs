﻿using VetAppointment.Application;
using VetAppointment.Domain.Models;


namespace VetAppointment.Infrastructure.Generics.GenericRepositories
{
    public class AppointmentRepository : Repository<Appointment>
    {
        public AppointmentRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
