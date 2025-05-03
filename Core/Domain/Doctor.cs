using HospitalManagentApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace HospitalManagentApi.Core.Domain
{
    public class Doctor
    {
        public int Id { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }

        public string FullName
        {
            get { return FullName; }
            set { FullName = fName + lName; }
        }

        public string Phone_Number { get; set; }
        public string Specialty { get; set; }
        public IList<Appointment> Appointment { get; set; }
        public IList<Clinic> Clinic { get; set; }
    }



}
