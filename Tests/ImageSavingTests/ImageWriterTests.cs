using System.IO;
using WebApp.Models;
using Xunit;

namespace Tests.ImageSavingTests
{
    public class ImageWriterTests
    {
        private ImageWriter imageWriter;

        public ImageWriterTests()
        {
            imageWriter = new ImageWriter();
        }

        [Fact]
        public void CreateFileWithGivenName()
        {
            imageWriter = new ImageWriter();

            imageWriter.SaveImage("image.png", "");

            Assert.True(File.Exists("image.png"));

            File.Delete("image.png");
        }
    }
}
