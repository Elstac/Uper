using System.IO;
using WebApp.Models;
using Xunit;

namespace Tests.ImageSavingTests
{
    public class ImageIdProviderTests
    {
        private FileIdProvider idProvider;
        private string path = "test/";

        public ImageIdProviderTests()
        {
            Directory.CreateDirectory("test/");
        }

        [Fact]
        public void GetFirstFreeIdIfNoFileIsPresent()
        {
            idProvider = new FileIdProvider();

            var @out = idProvider.GetId(path, ".png");

            Assert.Equal("0", @out);
        }

        [Fact]
        public void ReturnCharacterIfAllNubersAreTaken()
        {
            idProvider = new FileIdProvider();
            for (int i = 0; i < 10; i++)
                File.Create(path + i+".png").Close();

            var @out = idProvider.GetId(path, ".png");

            Assert.Equal("a", @out);

            for (int i = 0; i < 10; i++)
                File.Delete(path + i + ".png");
        }

        [Fact]
        public void ThrowExeptionIfGivenDirectoryDoesntExists()
        {
            var nPath = "notExistingDir/";
            if (Directory.Exists(nPath))
                Directory.Delete(nPath);

            idProvider = new FileIdProvider();

            Assert.Throws<DirectoryNotFoundException>(()=>idProvider.GetId(nPath, ".png"));
        }
    }
}
