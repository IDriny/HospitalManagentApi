namespace HospitalManagentApi.Core.Domain
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IList<Appointment> Appointment { get; set; }
        public IList<Diagnosis> Diagnoses { get; set; }
        public IList<Prescription> Prescriptions { get; set; }
        public IList<Laboratory> Laboratories { get; set; }
    }
}
