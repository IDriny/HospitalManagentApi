using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class ClinicDoctorRepo:GenericRepo<ClinicDoctor>,Core.Contracts.IClinicDoctorRepo
    {
        private readonly Persistence.HospitalDbContext _context;

        public ClinicDoctorRepo(Persistence.HospitalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<ClinicDoctor> GetDetailsAsync(int id)
        {
            return await _context.ClinicDoctors.Include(c => c.Doctor).Include(c => c.Clinic)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
