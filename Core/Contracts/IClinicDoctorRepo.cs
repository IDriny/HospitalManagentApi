using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.ClinicDoctor;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IClinicDoctorRepo:IGenericRepo<ClinicDoctor>
    {
        public Task<ClinicDoctor> GetDetailsAsync(int id);
    }
}
