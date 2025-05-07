using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IClinicDoctorRepo:IGenericRepo<ClinicDoctor>
    {
        public Task<ClinicDoctor> GetDetailsAsync(int id);
    }
}
