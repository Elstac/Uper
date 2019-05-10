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

            provider = new HtmlNotificationProvider(bodyMock.Object);
        }

        [Fact]
    }
}
