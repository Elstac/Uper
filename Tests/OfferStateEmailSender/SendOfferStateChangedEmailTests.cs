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
        private Mock<IMessageBodyProvider> bodyProviderMock;
        private Mock<IMessageBodyDictionary> bodyMock;

        public SendOfferStateChangedEmailTests()
        {
            bodyMock = new Mock<IMessageBodyDictionary>();
            bodyProviderMock = new Mock<IMessageBodyProvider>();
            bodyProviderMock.Setup(bp => bp.GetBody(It.IsAny<object[]>())).Returns(bodyMock.Object);

            serviceMock = new Mock<IEmailService>();

            sender = new OfferStateEmailSender(serviceMock.Object,bodyProviderMock.Object);
        }

        [Fact]
        public void PassUserNameToEmailService()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<string>(), It.IsAny<OfferStateChange>());

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), "usernameX", It.IsAny<string>(), It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void PassUperToEmailServiceAsSender()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<string>(), It.IsAny<OfferStateChange>());

            serviceMock.Verify(sm => sm.SendMail("Uper", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void RequestStateChangedAsMessageTypeIfStateChangeIsAccepted()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<string>(), OfferStateChange.RequestAccepted);

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), "RequestStateChanged", It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void OfferStateChangedAsMessageTypeIfStateChangeIsUserRemoved()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<string>(), OfferStateChange.UserRemoved);

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), "RequestStateChanged", It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void OfferStateChangedAsMessageTypeIfStateChangeIsDeleted()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<string>(), OfferStateChange.Deleted);

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), "OfferStateChanged", It.IsAny<IMessageBodyDictionary>()));
        }

        [Fact]
        public void GetMessageBodyFromProviderAndPassItToEmailService()
        {
            sender.SendOfferStateChangedEmail("usernameX", It.IsAny<string>(), It.IsAny<OfferStateChange>());

            serviceMock.Verify(sm => sm.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), bodyMock.Object));
        }
        [Fact]
        public void PassStateChangeLinkAndUsernameToBodyProvider()
        {
            sender.SendOfferStateChangedEmail("usernameX", "linkY", OfferStateChange.Deleted);

            bodyProviderMock.Verify(mp => mp.GetBody("usernameX","linkY",OfferStateChange.Deleted));
        }
    }
}
