using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class LabRepo:GenericRepo<Laboratory>,ILabRepo
    {
        private readonly HospitalDbContext _context;

        public LabRepo(HospitalDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Laboratory> GetLabDetailsAsync(int Id)
        {
            return await _context.Laboratory.Include(l => l.Patient).FirstOrDefaultAsync(l => l.Id == Id);
        }
    }
}
