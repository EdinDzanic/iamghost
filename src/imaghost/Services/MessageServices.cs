using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace imaghost.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MimeMessage msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("I'am a ghost", "no-reply@imaghost.com"));
            msg.To.Add(new MailboxAddress("", email));
            msg.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = message;
            msg.Body = builder.ToMessageBody();

            using (SmtpClient smtpClient = new SmtpClient())
            {
                NetworkCredential credentials = new NetworkCredential("nitrodino666@gmail.com", "e26dzsefo");

                try
                {
                    smtpClient.Connect("smtp.gmail.com", Convert.ToInt32(465), true);
                    smtpClient.Authenticate(credentials);

                    await smtpClient.SendAsync(msg);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    smtpClient.Disconnect(true);
                }
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
