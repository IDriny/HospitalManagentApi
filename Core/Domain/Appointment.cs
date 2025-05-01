namespace HospitalManagentApi.Core.Domain
{
    public class Appointment
    {
        public int ID { get; set; }

        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime Date { get; set; }
    }
}
