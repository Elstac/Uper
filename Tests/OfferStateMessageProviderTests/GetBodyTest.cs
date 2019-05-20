using WebApp.Models.TravellChangeEmail;
using Xunit;

namespace Tests.OfferStateMessageProviderTests
{
    public class GetBodyTests
    {
        private OfferStateMessageProvider messageProvider;

        public GetBodyTests()
        {
            messageProvider = new OfferStateMessageProvider();
        }

        [Fact]
        public void AddNameReplacament()
        {
            var @out = messageProvider.GetBody("username","");

            Assert.Equal("username", @out.GetReplacement("Name"));
        }

        [Fact]
        public void AddLinkReplacament()
        {
            var @out = messageProvider.GetBody("username","link");

            Assert.Equal("link", @out.GetReplacement("Link"));
        }
    }
}
