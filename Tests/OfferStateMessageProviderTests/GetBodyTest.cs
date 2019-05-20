using WebApp.Models.TravellChangeEmail;
using Xunit;
using Moq;

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
            var @out = messageProvider.GetBody("username","",It.IsAny<OfferStateChange>());

            Assert.Equal("username", @out.GetReplacement("{Name}"));
        }

        [Fact]
        public void AddLinkReplacament()
        {
            var @out = messageProvider.GetBody("username","link", It.IsAny<OfferStateChange>());

            Assert.Equal("link", @out.GetReplacement("{Link}"));
        }

        [Fact]
        public void AddOldSateReplacementAsPendingAndNewStateAsDeletedIfOfferDeleted()
        {
            var @out = messageProvider.GetBody("username", "link", OfferStateChange.Deleted);

            Assert.Equal("Pending", @out.GetReplacement("{OldState}"));
            Assert.Equal("Deleted", @out.GetReplacement("{NewState}"));
        }

        [Fact]
        public void AddOldSateAsAcceptedAndNewStateAsRemovedIfUserRemoved()
        {
            var @out = messageProvider.GetBody("username", "link", OfferStateChange.UserRemoved);

            Assert.Equal("Accepted", @out.GetReplacement("{OldState}"));
            Assert.Equal("Removed", @out.GetReplacement("{NewState}"));
        }

        [Fact]
        public void AddOldSateAsPendingAndNewStateAsAcceptedIfRequest()
        {
            var @out = messageProvider.GetBody("username", "link", OfferStateChange.RequestAccepted);

            Assert.Equal("Pending", @out.GetReplacement("{OldState}"));
            Assert.Equal("Accepted", @out.GetReplacement("{NewState}"));
        }
    }
}
