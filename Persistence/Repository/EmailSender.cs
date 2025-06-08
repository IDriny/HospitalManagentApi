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
        public Task SendEmailAsync(string email, string subject,string user)
        {
            var mail = _configuration.GetValue<string>("SmtpSettings:Email");
            var password= _configuration.GetValue<string>("SmtpSettings:Password");
            var host = _configuration.GetValue<string>("SmtpSettings:Host");
            var port = _configuration.GetValue<int>("SmtpSettings:port");
            var smtpClient =new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials=false;
            smtpClient.Credentials = new NetworkCredential(mail, password);

            string body =
                "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        body {\r\n            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\r\n            background-color: #f7f9fc;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n        .email-container {\r\n            max-width: 600px;\r\n            margin: auto;\r\n            background-color: #ffffff;\r\n            border-radius: 10px;\r\n            padding: 30px;\r\n            box-shadow: 0 5px 15px rgba(0,0,0,0.05);\r\n        }\r\n        .header {\r\n            text-align: center;\r\n            padding-bottom: 20px;\r\n        }\r\n        .header h1 {\r\n            color: #2f54eb;\r\n            margin-bottom: 5px;\r\n        }\r\n        .content {\r\n            font-size: 16px;\r\n            color: #333333;\r\n            line-height: 1.6;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            margin-top: 25px;\r\n            padding: 12px 24px;\r\n            background-color: #2f54eb;\r\n            color: #ffffff;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            font-weight: bold;\r\n        }\r\n        .footer {\r\n            margin-top: 40px;\r\n            font-size: 13px;\r\n            color: #999999;\r\n            text-align: center;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            <h1>Welcome to Samadoun NP Hospital!</h1>\r\n            <p>Your health, our priority.</p>\r\n        </div>\r\n        <div class=\"content\">\r\n            <p>Dear <strong>"+$"{user}"+ "</strong>,</p>\r\n            <p>\r\n                We’re thrilled to welcome you to <strong>Samadoun NP Hospital</strong>. Thank you for registering with us!\r\n                Whether you're here to book appointments, check medical records, or consult our doctors,\r\n                we’re committed to making your experience smooth and secure.\r\n            </p>\r\n            <p>\r\n                To get started, simply log into your dashboard and explore the services available to you.\r\n            </p>\r\n            <p style=\"text-align: center;\">\r\n                <a href=\"https://hospital-project-ruddy.vercel.app/\" class=\"button\">Go to Website</a>\r\n            </p>\r\n            <p>\r\n                If you have any questions, don’t hesitate to contact our support team. We're always here to help.\r\n            </p>\r\n            <p>Warm regards,<br/>The Samadoun NP Hospital Team</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            &copy; 2025 HospitalX. All rights reserved.\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";

            var msg = new MailMessage(mail!, email, subject, body);
            msg.IsBodyHtml=true;
            return smtpClient.SendMailAsync(msg);

        }
    }
}
