﻿namespace VetAppointment.API.DTOs.Update
{
    public class UpdateClientDto
    {
        public UpdateClientDto(Guid id, string name, string phoneNumber, string emailAddress, string address, Guid medicId)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
            MedicId = medicId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
                     
        public Guid MedicId { get; set; }
    }
}