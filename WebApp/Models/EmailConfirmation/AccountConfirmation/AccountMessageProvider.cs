using WebApp.Services;

namespace WebApp.Models.EmailConfirmation
{
    public class AccountMessageProvider : IMessageBodyProvider
    {
        public IMessageBodyDictionary GetBody(params object[] par)
        {
            return new MessageBodyDictionary()
                .AddReplacement(par[0].ToString(), "{Name}")
                .AddReplacement("to confirm your account", "{Purpose}");
        }
    }
}
