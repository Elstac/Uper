using WebApp.Exceptions;
using WebApp.Models;
using Xunit;

namespace Tests
{
    public class EmailAddressValidatorTest
    {
        private EmailAddressValidator validator;

        public EmailAddressValidatorTest()
        {
            validator = new EmailAddressValidator();
        }

        [Fact]
        public void PassCorrectAddress()
        {
            Assert.True(validator.ValidateEmailAddress("test@testmail.com"));
        }

        [Fact]
        public void ValidateAddressWithDot()
        {
            Assert.True(validator.ValidateEmailAddress("te.st@testmail.com"));
        }

        [Theory]
        [InlineData("@testmail.com")]
        [InlineData("testtestmail.com")]
        [InlineData("test@.com")]
        [InlineData("test@testmail")]
        [InlineData("test@testmail.")]
        [InlineData("t@est@testmail.com")]
        public void ThrowException(string address)
        {
            Assert.Throws<InvalidEmailAddressException>(() => validator.ValidateEmailAddress(address));
        }
    }
}
