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

        [Theory]
        [InlineData("@testmail.com")]
        [InlineData("test@.com")]
        [InlineData("test@testmail")]
        public void ThrowException(string address)
        {
            Assert.Throws<InvalidEmailAddressException>(() => validator.ValidateEmailAddress(address));
        }
    }
}
