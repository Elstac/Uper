using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Diagnostics.CodeAnalysis;
namespace WebApp.Services
{
    [ExcludeFromCodeCoverage]
    public class GmailSmtpClientProvider : ISmtpClientProvider
    {
        private SmtpClient client;

        public void Connect(string username, string password)
        {
            client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            client.Connect("smtp.gmail.com", 465, true);

            client.Authenticate(username, password);
        }

        public void Diconnect()
        {
            client.Disconnect(true);
        }

        public void SendMessage(MimeMessage message)
        {
            if (!client.IsConnected)
                throw new InvalidOperationException("SmtpClient disconnected");

            client.Send(message);
        }
    }
}
