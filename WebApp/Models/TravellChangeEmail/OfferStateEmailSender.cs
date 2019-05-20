using System.Collections.Generic;
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
        void SendOfferStateChangedEmail(string username,OfferStateChange stateChange);
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

        public void SendOfferStateChangedEmail(string username, OfferStateChange stateChange)
        {
            string msgType = stateTypeDict[stateChange];

            emailService.SendMail("Uper", username, msgType, bodyProvider.GetBody(stateChange));
        }
    }
}
