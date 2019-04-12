using System;
using WebApp.Exceptions;
using WebApp.Services;
using Xunit;

namespace Tests
{

    public class MessageBuilderTests
    {
        private MessageBuilder messageBuilder = new MessageBuilder();
        private string template = "<h1>Head</h1>" +
                                 "<p>Body</p>" +
                                 "<p>Footer</p>";
        [Fact]
        public void ThrowNullReferenceExceptionWhenBuildWithoutTemplateSet()
        {
            messageBuilder.Template = null;

            Assert.Throws<NullReferenceException>(() => messageBuilder.BuildMessage());
        }

        [Fact]
        public void ReturnCorrectMessage()
        {
            messageBuilder.Template=template;

            messageBuilder.Head = "A";
            messageBuilder.BodyParts = new System.Collections.Generic.List<string> { "B" };
            messageBuilder.Footer = "C";

            var output = messageBuilder.BuildMessage();
            var expected = "<h1>A</h1>" +
                           "<p>B</p>" +
                           "<p>C</p>";

            Assert.Equal(expected,output.HtmlBody.ToString());
        }

        [Fact]
        public void ReturnCorrectMessageWithMultipleBodyParts()
        {
            messageBuilder.Template = "<h1>Head</h1>" +
                                 "<h2>Body1</h2>" +
                                 "<p>Body2</p>" +
                                 "<p>Footer</p>";

            messageBuilder.Head = "A";
            messageBuilder.BodyParts = new System.Collections.Generic.List<string> { "B","B2"};
            messageBuilder.Footer = "C";

            var output = messageBuilder.BuildMessage();
            var expected = "<h1>A</h1>" +
                                 "<h2>B</h2>" +
                                 "<p>B2</p>" +
                                 "<p>C</p>";

            Assert.Equal(expected, output.HtmlBody.ToString());
        }

        [Fact]
        public void ThrowMessageExceptionWhenTooManyBodyParts()
        {
            messageBuilder.Template = template;

            messageBuilder.BodyParts = new System.Collections.Generic.List<string> { "B1", "B2", "B3" };

            Assert.Throws<MessageException>(() => messageBuilder.BuildMessage());
        }

        [Theory]
        [InlineData("<p>Body</p><p>Footer</p>")]
        [InlineData("<h1>Head</h1><p>Body</p>")]
        [InlineData("<h1>Head</h1><p>Footer</p>")]
        public void ThrowMessageExceptionWhenIncompatibileTemplate(string temp)
        {
            messageBuilder.Template = temp;

            messageBuilder.Head = "A";
            messageBuilder.BodyParts = new System.Collections.Generic.List<string> { "B" };
            messageBuilder.Footer = "C";

            Assert.Throws<MessageException>(() => messageBuilder.BuildMessage());
        }
    }
}
