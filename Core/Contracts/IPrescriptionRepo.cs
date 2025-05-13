using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IPrescriptionRepo:IGenericRepo<Prescription>
    {
        Task<Prescription> GetPrescriptionDetalils(int id);
    }
}
