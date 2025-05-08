namespace HospitalManagentApi.Models.Prescription
{
    public class BasePrescriptionModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
    }
}
