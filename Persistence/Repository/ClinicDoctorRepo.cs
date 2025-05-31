using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.ClinicDoctor;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class ClinicDoctorRepo:GenericRepo<ClinicDoctor>,IClinicDoctorRepo
    {
        private readonly Persistence.HospitalDbContext _context;
        private readonly IMapper _mapper;

        public ClinicDoctorRepo(Persistence.HospitalDbContext context,IMapper mapper):base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClinicDoctor> GetDetailsAsync(int id)
        {
            return await _context.ClinicDoctors.Include(c => c.Doctor).Include(c => c.Clinic)
                .FirstOrDefaultAsync(c => c.Id == id);

        }
    }
}
