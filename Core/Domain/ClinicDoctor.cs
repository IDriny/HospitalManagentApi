using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HospitalManagentApi.Core.Domain
{
    public class ClinicDoctor
    {
        public int Id { get; set; }

        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }


    }
}
