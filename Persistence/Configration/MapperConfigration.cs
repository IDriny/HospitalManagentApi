using AutoMapper;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Appointment;
using HospitalManagentApi.Models.Clinic;

namespace HospitalManagentApi.Persistence.Configration
{
    public class MapperConfigration:Profile
    {
        public MapperConfigration()
        {
            //Appointment Mapping
            CreateMap<Appointment, BaseAppointmentModel>().ReverseMap();
            CreateMap<Appointment, GetAppointmentModel>().ReverseMap();
            CreateMap<Appointment, GetAppointmentInfoModel>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentModel>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentModel>().ReverseMap();


            //Clinic Mapping
            CreateMap<Clinic, BaseClinicModel>().ReverseMap();
            CreateMap<Clinic, CreateClinicModel>().ReverseMap();
            CreateMap<Clinic, GetClinicModel>().ReverseMap();
            CreateMap<Clinic, UpdateCLinicModel>().ReverseMap();
        }
    }
}
