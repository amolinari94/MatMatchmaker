using BlazorApp.EmailSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace BlazorApp.EmailSystem.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailConfig;
        public MailService(IOptions<MailSettings> mailConfig)
        {
            _mailConfig = mailConfig.Value;
        }

        public async Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient(_mailConfig.Host, _mailConfig.Port);
            smtp.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
            message.From = new MailAddress(_mailConfig.FromEmail);
            message.To.Add(new MailAddress(ToEmail));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = HTMLBody; Console.WriteLine("message constructed...");
            smtp.Port = _mailConfig.Port;
            smtp.Host = _mailConfig.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;Console.WriteLine("SMTP Host configured...");
            await smtp.SendMailAsync(message);
            Console.WriteLine("**Success**");
        }
    }
}