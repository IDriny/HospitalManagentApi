using AutoMapper;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Appointment;

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
            CreateMap<Appointment, UpdateAppointmentModel>().ReverseMap();



        }
    }
}
