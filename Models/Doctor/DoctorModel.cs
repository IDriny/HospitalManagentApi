using HospitalManagentApi.Models.Appointment;
using HospitalManagentApi.Models.ClinicDoctor;

namespace HospitalManagentApi.Models.Doctor
{
    public class DoctorModel: BaseDoctorModel
    {
        public int Id { get; set; }
        public List<GetAppointmentModel> Appointment { get; set; }
        public List<GetClinicDoctorModel> Clinic { get; set; }
    }
}
