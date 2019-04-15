using System;
using System.Text.RegularExpressions;
using WebApp.Exceptions;
using WebApp.Services;
using Xunit;
using Moq;

namespace Tests
{

    public class ContentBuilderTests
    {
        private Mock<IMessageBodyDictionary> bodyMock;
        private ContentBuilder messageBuilder = new ContentBuilder(new Regex(@"\{\w+\}"));
        private string template = "<h1>{Head}</h1>" +
                                 "<p>{Body1}</p>" +
                                 "<p>{Footer}</p>";

        public ContentBuilderTests()
        {
            bodyMock = new Mock<IMessageBodyDictionary>();
            bodyMock.Setup(m => m.GetReplacement("{Head}")).Returns("A");
            bodyMock.Setup(m => m.GetReplacement("{Body1}")).Returns("B");
            bodyMock.Setup(m => m.GetReplacement("{Footer}")).Returns("C");
        }
        [Fact]
        public void ThrowNullReferenceExceptionWhenBuildWithoutTemplateSet()
        {
            Assert.Throws<NullReferenceException>(() => messageBuilder.BuildContent(null,bodyMock.Object));
        }

        [Fact]
        public void ReturnCorrectMessage()
        {
            var output = messageBuilder.BuildContent(template ,bodyMock.Object);
            var expected = "<h1>A</h1>" +
                           "<p>B</p>" +
                           "<p>C</p>";

            Assert.Equal(expected,output);
        }
        
        [Theory]
        [InlineData("<p>{Body}</p><p>{Footer}</p>")]
        [InlineData("<h1>{Head}</h1><p>{Body|}</p>")]
        [InlineData("<h1>{Head}</h1><p>{Footer}</p>")]
        public void ThrowMessageExceptionWhenIncompatibileTemplateAndBodydictionary(string temp)
        {
            Assert.Throws<MessageException>(() => messageBuilder.BuildContent(temp,bodyMock.Object));
        }
    }
}
