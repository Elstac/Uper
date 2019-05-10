using Xunit;
using Moq;
using WebApp.Models.HtmlNotifications;

namespace Tests
{
    public class ResultNotificationHtmlProviderTests
    {
        private HtmlNotificationProvider htmlNotificationProvider;

        public ResultNotificationHtmlProviderTests()
        {
            htmlNotificationProvider = new HtmlNotificationProvider();
        }
        [Fact]
        public void ReturnValidHtmlStringUsingGivenClassAndContent()
        {

        }

    }
}
