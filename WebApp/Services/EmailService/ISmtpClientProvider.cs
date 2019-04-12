using MimeKit;

namespace WebApp.Services
{
    public interface ISmtpClientProvider
    {
        void Connect(string username, string password);
        void Diconnect();
        void SendMessage(MimeMessage message);
    }
}