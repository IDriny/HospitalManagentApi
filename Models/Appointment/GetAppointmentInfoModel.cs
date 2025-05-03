namespace HospitalManagentApi.Models.Appointment
{
    public class GetAppointmentInfoModel:BaseAppointmentModel
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName{ get; set; }
    }
}
