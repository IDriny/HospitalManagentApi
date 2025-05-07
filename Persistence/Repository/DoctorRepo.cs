using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class DoctorRepo:GenericRepo<Doctor>,IDoctorRepo
    {
        private readonly HospitalDbContext _context;

        public DoctorRepo(HospitalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Doctor> GetDetailsAsync(int id)
        {
            return await _context.Doctor.Include(d => d.Appointment).FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
