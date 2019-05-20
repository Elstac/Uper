using System.Collections.Generic;
using WebApp.Data;
using WebApp.Models.EmailConfirmation;
using WebApp.Services;
public enum OfferStateChange
{
    RequestAccepted,
    UserRemoved,
    Deleted
}
namespace WebApp.Models.TravellChangeEmail
{
    public interface IOfferStateEmailSender
    {
        void SendOfferStateChangedEmail(ApplicationUser user, string urlToTrip, OfferStateChange stateChange);
        void SendOfferStateChangedEmail(IEnumerable<ApplicationUser> users, string urlToTrip, OfferStateChange stateChange);
    }

    public class OfferStateEmailSender : IOfferStateEmailSender
    {
        private IEmailService emailService;
        private IMessageBodyProvider bodyProvider;

        private Dictionary<OfferStateChange, string> stateTypeDict;

        public OfferStateEmailSender(IEmailService emailService, IMessageBodyProvider bodyProvider)
        {
            this.emailService = emailService;
            this.bodyProvider = bodyProvider;


            stateTypeDict = new Dictionary<OfferStateChange, string>();
            stateTypeDict.Add(OfferStateChange.UserRemoved, "RequestStateChanged");
            stateTypeDict.Add(OfferStateChange.RequestAccepted, "RequestStateChanged");
            stateTypeDict.Add(OfferStateChange.Deleted, "OfferStateChanged");
        }

        public void SendOfferStateChangedEmail(ApplicationUser user, string urlToTrip, OfferStateChange stateChange)
        {
            string msgType = stateTypeDict[stateChange];

            emailService.SendMail("Uper", user.Email, msgType, bodyProvider.GetBody(user.UserName,urlToTrip,stateChange));
        }

        public void SendOfferStateChangedEmail(IEnumerable<ApplicationUser> users, string urlToTrip, OfferStateChange stateChange)
        {
            foreach (var user in users)
            {
                SendOfferStateChangedEmail(user, urlToTrip, stateChange);
            }
        }
    }
}
