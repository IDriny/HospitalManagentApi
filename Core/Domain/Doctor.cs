using HospitalManagentApi.Models.User;
using HospitalManagentApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagentApi.Core.Domain
{
    public class Doctor
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string FullName { get; set; }
        public string Phone_Number { get; set; }
        public string Specialty { get; set; }
        public string Schdule { get; set; }

        public IList<Appointment> Appointment { get; set; }
        public IList<ClinicDoctor> Clinic { get; set; }
    }



}
