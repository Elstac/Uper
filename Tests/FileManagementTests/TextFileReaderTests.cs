using System;
using System.IO;
using WebApp.Models.FileManagement;
using Xunit;

namespace Tests.FileManagementTests
{
    public class TextFileReaderTests:IDisposable
    {
        private TextFileReader textFileReader;
        private string dirPath = "TextFileReaderTests/";
        private string filePath= "test.txt";

        public TextFileReaderTests()
        {
            textFileReader = new TextFileReader();

            Directory.CreateDirectory(dirPath);

            using (var sr = new StreamWriter(dirPath + filePath))
            {
                sr.Write("Content content");
            }


        }

        public void Dispose()
        {
            Directory.Delete(dirPath,true);
        }

        [Fact]
        public void ReturnFileContentAsString()
        {
            var @out = textFileReader.ReadFileContent(dirPath+filePath);

            Assert.Equal("Content content", @out);
        }
    }
}
