namespace HospitalManagentApi.Models.Diagnosis
{
    public class BaseDiagnosisModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }

    }
}
