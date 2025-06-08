namespace HospitalManagentApi.Core.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync (string email , string subject , string user );

    }
}
