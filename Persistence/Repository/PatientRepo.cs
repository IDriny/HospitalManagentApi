using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class PatientRepo:GenericRepo<Patient>,IPatientRepo
    {
        private readonly HospitalDbContext _context;

        public PatientRepo(HospitalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Patient> GetDetailsAsync(int id)
        {
            return await _context.Patient.Include(p => p.Appointment).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
