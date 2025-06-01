using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class AppointmentRepo:GenericRepo<Appointment>,IAppointmentRepo
    {
        private readonly Persistence.HospitalDbContext _context;

        public AppointmentRepo(Persistence.HospitalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Appointment> GetDetailsAsync(int id)
        {
            return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<List<Appointment>> GetAllUserAppointment(int id)
        {
            return await _context.Appointments.Include(ap => ap.Patient).Include(ap=>ap.Doctor).Where(ap => ap.PatientId == id).ToListAsync();
        }
    }
}
