using System.Reflection.Metadata.Ecma335;

namespace HospitalManagentApi.Core.Domain
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
    }
}
