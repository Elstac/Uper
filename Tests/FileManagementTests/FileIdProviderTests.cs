using System;
using System.IO;
using WebApp.Models.FileManagement;
using Xunit;

namespace Tests.FileManagementTests
{
    public class FileIdProviderTests:IDisposable
    {
        private FileIdProvider idProvider;
        private string path = "FileIdProviderTests/";

        public FileIdProviderTests()
        {
            Directory.CreateDirectory(path);
        }

        public void Dispose()
        {
            Directory.Delete(path, true);
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

            Assert.Equal("A", @out);

            for (int i = 0; i < 10; i++)
                File.Delete(path + i + ".png");
        }

        [Fact]
        public void AddNextCharacterIfAllPossibleIdCombitaionTaken()
        {
            idProvider = new FileIdProvider();
            for (int i = 48; i < 91; i++)
                if(Char.IsLetterOrDigit((char)i))
                    File.Create(path + (char)i + ".png").Close();

            var @out = idProvider.GetId(path, ".png");

            Assert.Equal("00", @out);

            for (int i = 48; i < 91; i++)
                if (Char.IsLetterOrDigit((char)i))
                    File.Delete(path + (char)i + ".png");
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
