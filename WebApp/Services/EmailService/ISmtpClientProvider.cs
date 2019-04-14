using MimeKit;

namespace WebApp.Services
{
    /// <summary>
    /// Provides connection to SmtpCliet
    /// </summary>
    public interface ISmtpClientProvider
    {
        void Connect(string username, string password);
        void Diconnect();
        void SendMessage(MimeMessage message);
    }
}