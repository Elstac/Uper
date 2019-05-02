using System;
using System.IO;
using System.Text;
using WebApp.Models.FileManagement;
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

            imageWriter.SaveFile("image.png", "");

            Assert.True(File.Exists("image.png"));

            File.Delete("image.png");
        }

        [Fact]
        public void SaveConvertedFromBase64DataToFile()
        {
            imageWriter = new ImageWriter();
            var expected = "imagedata";
            var encoded = Encoding.UTF8.GetBytes(expected);

            imageWriter.SaveFile("image.png", Convert.ToBase64String(encoded));

            string output;
            using (var sr = new StreamReader("image.png"))
            {
                output = sr.ReadToEnd();
            }

            Assert.Equal(expected, output);

            File.Delete("image.png");
        }
    }
}
