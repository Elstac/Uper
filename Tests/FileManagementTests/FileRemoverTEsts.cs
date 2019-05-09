using System.IO;
using WebApp.Models.FileManagement;
using Xunit;
namespace Tests.FileManagementTests
{
    public class FileRemoverTests: System.IDisposable
    {
        private FileRemover imageRemover;
        private string testPath = "FileRemoverTests/";

        public FileRemoverTests()
        {
            imageRemover = new FileRemover();
            Directory.CreateDirectory(testPath);
        }

        public void Dispose()
        {
            Directory.Delete(testPath, true);
        }

        [Fact]
        public void RemoveOnlyFileWithGivenId()
        {
            imageRemover = new FileRemover();
            File.Create(testPath + "0.png").Close();
            File.Create(testPath + "1.png").Close();
            
            imageRemover.RemoveImage("0", testPath, ".png");

            Assert.True(!File.Exists(testPath + "0.png"));
            Assert.True(File.Exists(testPath + "1.png"));

            File.Delete(testPath + "1.png");
        }
    }
}
