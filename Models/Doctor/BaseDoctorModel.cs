namespace HospitalManagentApi.Models.Doctor
{
    public abstract class BaseDoctorModel
    {
        public string FullName { get; set; }
        public string Phone_Number { get; set; }
        public string Specialty { get; set; }
        public string Schdule { get; set; }
    }
}
