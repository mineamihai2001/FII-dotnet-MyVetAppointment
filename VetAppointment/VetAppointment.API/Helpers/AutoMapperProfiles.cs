using AutoMapper;
using VetAppointment.Domain.Models;
using VetAppointment.API.DTOs.Create;

namespace VetAppointment.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Client, CreateClientDto>();
            CreateMap<Appointment, CreateAppointmentDto>();
            CreateMap<Medic, CreateMedicDto>();
            CreateMap<Bill, CreateBillDto>();
            CreateMap<Medicine, CreateMedicineDto>();
            CreateMap<Patient, CreatePatientDto>();
            CreateMap<Nurse, CreateNurseDto>();
            CreateMap<Room, CreateRoomDto>();
        }
    }
}
