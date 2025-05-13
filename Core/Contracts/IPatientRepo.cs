using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IPatientRepo:IGenericRepo<Patient>
    {
        public Task<Patient> GetDetailsAsync(int id);
        public Task<bool> ExistByEmailAsync(string Email);
    }
}
