namespace HospitalManagentApi.Models.Appointment
{
    public abstract class BaseAppointmentModel
    {
        public int ID { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
    }
}
