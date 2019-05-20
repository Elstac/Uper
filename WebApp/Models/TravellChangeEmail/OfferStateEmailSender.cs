namespace WebApp.Models.TravellChangeEmail
{
    public interface IOfferStateEmailSender
    {
        void SendOfferStateChangedEmail();
    }

    public class OfferStateEmailSender : IOfferStateEmailSender
    {
        public void SendOfferStateChangedEmail()
        {
            throw new System.NotImplementedException();
        }
    }
}
