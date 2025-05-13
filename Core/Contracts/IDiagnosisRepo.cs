using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IDiagnosisRepo:IGenericRepo<Diagnosis>
    {
        Task<Diagnosis> GetDiagnosisDetailsAsync(int Id);
    }
}
