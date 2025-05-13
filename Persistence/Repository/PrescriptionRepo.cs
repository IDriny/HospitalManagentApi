using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class PrescriptionRepo:GenericRepo<Prescription>,IPrescriptionRepo
    {
        private readonly HospitalDbContext _context;


        public PrescriptionRepo(HospitalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Prescription> GetPrescriptionDetalils(int id)
        {
            return await _context.Prescriptions.Include(p => p.Patient).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
