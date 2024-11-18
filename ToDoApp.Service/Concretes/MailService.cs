using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using ToDoApp.Models.SmtpSettings;

namespace ToDoApp.Service.Concretes;

public class MailService
{
    private readonly SmtpSettings _smtpSettings;

    public MailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string toAddress, string subject, string body)
    {
        using (var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
            smtpClient.EnableSsl = true; 

            var mailMessage = new MailMessage(_smtpSettings.FromAddress, toAddress, subject, body)
            {
                IsBodyHtml = true
            };
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}


