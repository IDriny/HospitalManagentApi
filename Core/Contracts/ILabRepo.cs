using HospitalManagentApi.Core.Domain;

namespace HospitalManagentApi.Core.Contracts
{
    public interface ILabRepo: IGenericRepo<Laboratory>
    {
        Task<Laboratory> GetLabDetailsAsync(int Id);
    }
}
