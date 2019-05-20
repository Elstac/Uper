using WebApp.Models.EmailConfirmation;
using WebApp.Services;

namespace WebApp.Models.TravellChangeEmail
{
    public class OfferStateMessageProvider : IMessageBodyProvider
    {
        public IMessageBodyDictionary GetBody(params object[] par)
        {
            return new MessageBodyDictionary()
                .AddReplacement(par[0].ToString(), "Name")
                .AddReplacement(par[1].ToString(), "OldState")
                .AddReplacement(par[2].ToString(), "NewState")
                .AddReplacement(par[3].ToString(), "Link");
        }
    }
}
