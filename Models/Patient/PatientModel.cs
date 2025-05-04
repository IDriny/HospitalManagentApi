using HospitalManagentApi.Models.Appointment;

namespace HospitalManagentApi.Models.Patient
{
    public class PatientModel: BasePatientModel
    {
        public int Id { get; set; }
        public List<GetAppointmentModel> Appointment { get; set; }
    }
}
