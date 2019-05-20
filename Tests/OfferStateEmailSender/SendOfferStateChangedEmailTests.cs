using Xunit;
using Moq;
using WebApp.Models.TravellChangeEmail;
using WebApp.Services;
using WebApp.Models.EmailConfirmation;

namespace Tests.OfferStateEmailSenderTests
{
    public class SendOfferStateChangedEmailTests
    {
        private OfferStateEmailSender sender;
        private Mock<IEmailService> serviceMock;

        public SendOfferStateChangedEmailTests()
        {
            serviceMock = new Mock<IEmailService>();

            sender = new OfferStateEmailSender(serviceMock.Object);
        }

        [Fact]
        public void PassUserNameToEmailService()
        {
            sender.SendOfferStateChangedEmail("usernameX",It.IsAny<OfferStateChange>());

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), "usernameX", It.IsAny<string>(), It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void PassUperToEmailServiceAsSender()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<OfferStateChange>());

            serviceMock.Verify(sm => sm.SendMail("Uper", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void RequestStateChangedAsMessageTypeIfStateChangeIsAccepted()
        {
            sender.SendOfferStateChangedEmail("usernameX", OfferStateChange.RequestAccepted);

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), "RequestStateChanged", It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void OfferStateChangedAsMessageTypeIfStateChangeIsUserRemoved()
        {
            sender.SendOfferStateChangedEmail("usernameX", OfferStateChange.UserRemoved);

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), "RequestStateChanged", It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void OfferStateChangedAsMessageTypeIfStateChangeIsDeleted()
        {
            sender.SendOfferStateChangedEmail("usernameX", OfferStateChange.Deleted);

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), "OfferStateChanged", It.IsAny<IMessageBodyDictionary>()));
        }
    }
}
