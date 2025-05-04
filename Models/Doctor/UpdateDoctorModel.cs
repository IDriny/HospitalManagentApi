namespace HospitalManagentApi.Models.Doctor
{
    public class UpdateDoctorModel: BaseDoctorModel
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
    }
}
