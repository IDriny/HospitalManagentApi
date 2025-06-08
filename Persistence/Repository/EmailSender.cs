using System.Net;
using System.Net.Mail;
using HospitalManagentApi.Core.Contracts;


namespace HospitalManagentApi.Persistence.Repository
{
    public class EmailSender:IEmailSender
    {
        private readonly IConfiguration _configuration;
        
        public EmailSender(IConfiguration configuration)
        {
            this._configuration = configuration;
            
        }
        public Task SendEmailAsync(string email, string subject, string body)
        {
            var mail = _configuration.GetValue<string>("SmtpSettings:Email");
            var password= _configuration.GetValue<string>("SmtpSettings:Password");
            var host = _configuration.GetValue<string>("SmtpSettings:Host");
            var port = _configuration.GetValue<int>("SmtpSettings:port");
            var smtpClient =new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials=false;
            smtpClient.Credentials = new NetworkCredential(mail, password);
            var msg = new MailMessage(mail!, email, subject, body);
            msg.IsBodyHtml=true;
            return smtpClient.SendMailAsync(msg);

        }
    }
}
