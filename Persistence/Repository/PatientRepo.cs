using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagentApi.Persistence.Repository
{
    public class PatientRepo:GenericRepo<Patient>,IPatientRepo
    {
        private readonly HospitalDbContext _context;
        private readonly IMapper _mapper;

        public PatientRepo(HospitalDbContext context,IMapper mapper):base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Patient> GetDetailsAsync(string Email)
        {
            return await _context.Patient.Include(p => p.Appointment).FirstOrDefaultAsync(p => p.Email == Email);
        }

        public async Task<bool> ExistByEmailAsync(string Email)
        {
            var patient= await _context.Patient.FirstOrDefaultAsync(p => p.Email == Email);
            return patient != null;
        }

        //public async Task<Patient> AddUserPatient(CreatePatientModel newPatient)
        //{
        //    var patient = _mapper.Map<Patient>(newPatient);
        //    patient.FullName = newPatient.FirstName + " " + newPatient.LastName;
        //    await _context.AddAsync(patient);
        //    await _context.SaveChangesAsync();
        //    return patient;
        //}
    }
}
