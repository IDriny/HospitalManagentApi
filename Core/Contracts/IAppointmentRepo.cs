using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Persistence;

namespace HospitalManagentApi.Core.Contracts
{
    public interface IAppointmentRepo:IGenericRepo<Appointment>
    {
        Task<Appointment> GetDetailsAsync(int id);
        Task<List<Appointment>> GetAllUserAppointment(int id);
    }
}
