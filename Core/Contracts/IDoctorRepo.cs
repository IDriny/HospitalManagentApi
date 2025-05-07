using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IDoctorRepo:IGenericRepo<Doctor>
    {
        public Task<Doctor> GetDetailsAsync(int id);
    }
}
