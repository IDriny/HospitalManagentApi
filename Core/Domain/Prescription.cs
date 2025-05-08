namespace HospitalManagentApi.Core.Domain
{
    public class Prescription
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
