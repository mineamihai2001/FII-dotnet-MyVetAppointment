﻿using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Models
{

    public class Room
    {
        public Room(string type, int roomNumber, int capacity)
        {
            Id = Guid.NewGuid();
            Type = type;
            RoomNumber = roomNumber;
            Capacity = capacity;
        }

        public Guid Id { get; private set; }
        public int RoomNumber { get; private set; }
        public string Type { get; private set; }
        public int Capacity { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();

        public Result RegisterAppointmentsToRoom(List<Appointment> appointments)
        {
            if (!appointments.Any())
            {
                return Result.Failure("Add at least an appointment to the room");
            }

            appointments.ForEach(appointment =>
            {
                appointment.AttachAppointmentToRoom(this);
                Appointments.Add(appointment);
            });

            return Result.Success();
        }
    }

}