using Moq;
using System;
using System.Text.RegularExpressions;
using WebApp.Services;
using Xunit;

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
        public void ThrowArgumentNullExceptionWhenBuildWithoutTemplateSet()
        {
            Assert.Throws<ArgumentNullException>(() => messageBuilder.BuildContent(null,bodyMock.Object));
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
        [InlineData("<p>{Body1}</p><p>{Footer}</p>", "<p>B</p><p>C</p>")]
        [InlineData("<h1>{Head}</h1><p>{Body1}</p>", "<h1>A</h1><p>B</p>")]
        [InlineData("<h1>{Head}</h1><p>{Footer}</p>", "<h1>A</h1><p>C</p>")]
        public void ReturnCorrectContentWhenTemplateAndBodydictionaryAreIncompatibile(string temp, string expected)
        {
            var output = messageBuilder.BuildContent(temp, bodyMock.Object);

            Assert.Equal(expected, output);
        }
    }
}
