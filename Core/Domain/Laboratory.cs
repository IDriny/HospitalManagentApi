using Microsoft.Build.Framework;

namespace HospitalManagentApi.Core.Domain
{
    public class Laboratory
    {
        public int Id { get; set; }
        [Required]
        public string  Test { get; set; }
        public string Result { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
