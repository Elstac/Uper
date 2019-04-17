using System.Threading.Tasks;

namespace WebApp.Models.EmailConfirmation
{
    interface IEmailConfirmatorAsync
    {
        void SendConfirmationEmail(string Id, string url);
        Task ConfirmEmailAsync(string Id, string token, params object[] parameters);
    }
}
