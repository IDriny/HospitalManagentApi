using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class DiagnosisRepo:GenericRepo<Diagnosis>,Core.Contracts.IDiagnosisRepo
    {
        private readonly Persistence.HospitalDbContext _context;

        public DiagnosisRepo(HospitalDbContext context):base (context)
        {
            _context = context;
        }


        public async Task<Diagnosis> GetDiagnosisDetailsAsync(int Id)
        {
           return await _context.Diagnoses.Include(d => d.Patient).FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}
