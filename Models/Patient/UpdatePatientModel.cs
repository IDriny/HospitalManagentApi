namespace HospitalManagentApi.Models.Patient
{
    public class UpdatePatientModel: BasePatientModel
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
    }
}
