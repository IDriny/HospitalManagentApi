namespace HospitalManagentApi.Models.Doctor
{
    public class UpdateDoctorModel
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string Phone_Number { get; set; }
        public string Specialty { get; set; }
    }
}
