using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ClassroomScheduler.Services
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }

        public async Task Execute(string email, string subject, string message)
        {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuration["EmailSenderSettings:Email"], _configuration["EmailSenderSettings:DisplayName"])
                };

                mail.To.Add(new MailAddress(email));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_configuration["EmailSenderSettings:Host"], 
                    Int16.Parse(_configuration["EmailSenderSettings:Port"])))
                {
                    smtp.Credentials = new NetworkCredential(_configuration["EmailSenderSettings:Email"], 
                                _configuration["EmailSenderSettings:Password"]);

                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
        }

    }
}
