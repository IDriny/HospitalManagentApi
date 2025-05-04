namespace HospitalManagentApi.Core.Domain
{
    public class Patient
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string FullName { get; set; }
        public string Phone_Number { get; set; }
        public string Email { get; set; }
        public IList<Appointment> Appointment { get; set; }
    }
}
