using WebApp.Models.EmailConfirmation;
using WebApp.Services;

namespace WebApp.Models.TravellChangeEmail
{
    public class OfferStateMessageProvider : IMessageBodyProvider
    {
        public IMessageBodyDictionary GetBody(params object[] par)
        {
            var ret = new MessageBodyDictionary()
                .AddReplacement(par[0].ToString(), "Name")
                .AddReplacement(par[1].ToString(), "Link");

            switch ((OfferStateChange)par[2])
            {
                case OfferStateChange.RequestAccepted:
                    ret = ret.AddReplacement("Pending", "OldState")
                             .AddReplacement("Accepted", "NewState");
                    break;
                case OfferStateChange.UserRemoved:
                    ret = ret.AddReplacement("Accepted", "OldState")
                             .AddReplacement("Removed", "NewState");
                    break;
                case OfferStateChange.Deleted:
                    ret = ret.AddReplacement("Pending", "OldState")
                             .AddReplacement("Deleted", "NewState");
                    break;
            }

            return ret;
        }
    }
}
