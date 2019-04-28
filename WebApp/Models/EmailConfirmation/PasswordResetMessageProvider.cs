using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    public class PasswordResetMessageProvider : IMessageBodyProvider
    {
        public IMessageBodyDictionary GetBody(params object[] par)
        {
            return new MessageBodyDictionary()
                .AddReplacement(par[0].ToString(), "{Name}")
                .AddReplacement("to reset your password", "{Purpose}");
        }
    }
}
