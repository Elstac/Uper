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

        public OfferStateEmailSender(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public void SendOfferStateChangedEmail(string username, OfferStateChange stateChange)
        {
            string msgType ="";
            switch (stateChange)
            {
                case OfferStateChange.RequestAccepted:
                    msgType = "RequestStateChanged";
                    break;
                case OfferStateChange.UserRemoved:
                    msgType = "RequestStateChanged";
                    break;
                case OfferStateChange.Deleted:
                    msgType = "OfferStateChanged";
                    break;
            }

            emailService.SendMail("Uper", username, msgType, null);
        }
    }
}
