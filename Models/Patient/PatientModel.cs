using HospitalManagentApi.Models.Appointment;
using HospitalManagentApi.Models.Diagnosis;
using HospitalManagentApi.Models.Laboratory;
using HospitalManagentApi.Models.Prescription;

namespace HospitalManagentApi.Models.Patient
{
    public class PatientModel: BasePatientModel
    {
        public int Id { get; set; }
        public List<GetAppointmentModel> Appointment { get; set; }
        public List<GetDiagnosisModel> Diagnoses { get; set; }
        public List<GetPrescriptionModel> Prescriptions { get; set; }
        public List<GetLabModel> Laboratories { get; set; }
    }
}
