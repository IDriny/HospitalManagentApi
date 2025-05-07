using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Persistence.Repository
{
    public class ClinicRepo:GenericRepo<Clinic>,IClinicRepo
    {
        private readonly HospitalDbContext _context;

        public ClinicRepo(HospitalDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
