using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IPatientRepo:IGenericRepo<Patient>
    {
        public Task<Patient> GetDetailsAsync(int id);
    }
}
