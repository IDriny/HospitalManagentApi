using AutoMapper;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Appointment;
using HospitalManagentApi.Models.Clinic;
using HospitalManagentApi.Models.ClinicDoctor;
using HospitalManagentApi.Models.Diagnosis;
using HospitalManagentApi.Models.Doctor;
using HospitalManagentApi.Models.Patient;

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

            //ClinicDoctor Mapping
            CreateMap<ClinicDoctor, BaseClinicDoctorModel>().ReverseMap();
            CreateMap<ClinicDoctor, CreateClinicDoctorModel>().ReverseMap();
            CreateMap<ClinicDoctor, ClinicDoctorModel>().ReverseMap();
            CreateMap<ClinicDoctor, GetClinicDoctorModel>().ReverseMap();
            CreateMap<ClinicDoctor, UpdateClinicDoctorModel>().ReverseMap();

            //Doctor Mapping
            CreateMap<Doctor, BaseDoctorModel>().ReverseMap();
            CreateMap<Doctor, CreateDoctorModel>().ReverseMap();
            CreateMap<Doctor, GetDoctorModel>().ReverseMap();
            CreateMap<Doctor, DoctorModel>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorModel>().ReverseMap();

            //Patient Mapping
            CreateMap<Patient, BasePatientModel>().ReverseMap();
            CreateMap<Patient, GetPatientModel>().ReverseMap();
            CreateMap<Patient, PatientModel>().ReverseMap();
            CreateMap<Patient, UpdatePatientModel>().ReverseMap();
            CreateMap<Patient, CreatePatientModel>().ReverseMap();

            //Diagnosis Mapping
            CreateMap<Diagnosis, BaseDiagnosisModel>().ReverseMap();
            CreateMap<Diagnosis, GetDiagnosisModel>().ReverseMap();
            CreateMap<Diagnosis, DiagnosisModel>().ReverseMap();
            CreateMap<Diagnosis, CreateDiagnosisModel>().ReverseMap();
            CreateMap<Diagnosis, UpdateDiagnosisModel>().ReverseMap();




        }
    }
}
