using System.IO;
using WebApp.Models.ImageManagement;
using Xunit;
namespace Tests.ImageSavingTests
{
    public class ImageRemoverTests: System.IDisposable
    {
        private ImageRemover imageRemover;
        private string testPath = "test/";

        public ImageRemoverTests()
        {
            imageRemover = new ImageRemover();
            Directory.CreateDirectory(testPath);
        }

        public void Dispose()
        {
            Directory.Delete(testPath, true);
        }

        [Fact]
        public void RemoveOnlyFileWithGivenId()
        {
            imageRemover = new ImageRemover();
            File.Create(testPath + "0.png").Close();
            File.Create(testPath + "1.png").Close();
            
            imageRemover.RemoveImage("0", testPath, ".png");

            Assert.True(!File.Exists(testPath + "0.png"));
            Assert.True(File.Exists(testPath + "1.png"));

            File.Delete(testPath + "1.png");
        }
    }
}
