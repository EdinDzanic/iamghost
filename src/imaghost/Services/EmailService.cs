using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace imaghost.Services
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MimeMessage msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("I'am a ghost", "no-reply@imaghost.com"));
            msg.To.Add(new MailboxAddress("", email));
            msg.Subject = subject;
            msg.Body = new TextPart()
            {
                Text = message
            };

            using (SmtpClient smtpClient = new SmtpClient())
            {
                NetworkCredential credentials = new NetworkCredential( "nitrodino666@gmail.com", "e26dzsefo");

                smtpClient.Authenticate(credentials);
                smtpClient.Connect("smtp.gmail.com", Convert.ToInt32(587), true);

                await smtpClient.SendAsync(msg);

                smtpClient.Disconnect(true);
            }
        }
    }
}
