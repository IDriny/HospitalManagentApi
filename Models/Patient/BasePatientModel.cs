namespace HospitalManagentApi.Models.Patient
{
    public abstract class BasePatientModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
