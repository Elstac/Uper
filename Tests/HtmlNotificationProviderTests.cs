using Xunit;
using Moq;
using WebApp.Models.HtmlNotifications;
using Microsoft.AspNetCore.Http;

namespace Tests
{
    public class HtmlNotificationProviderTests
    {
        private HtmlNotificationProvider provider;
        private Mock<IHtmlNotificationBodyProvider> bodyMock;

        public HtmlNotificationProviderTests()
        {
            bodyMock = new Mock<IHtmlNotificationBodyProvider>();
            bodyMock.Setup(bm => bm.GetNotificationBody(It.IsAny<string>(), It.IsAny<string>())).Returns("body");

            provider = new HtmlNotificationProvider(bodyMock.Object);
        }

        [Fact]
        public void GetBodyFromProvider()
        {
            var sessMock = new Mock<ISession>();

            provider.SetNotification(sessMock.Object, "pClass", "content");

            bodyMock.Verify(bm => bm.GetNotificationBody("pClass", "content"));
        }

        [Fact]
        public void SaveHtmlBodyToSessionWithResultKey()
        {
            var sessMock = new Mock<ISession>();

            provider.SetNotification(sessMock.Object, "pClass", "content");

            sessMock.Verify(sm => sm.SetString("result","body"));
        }
    }
}
