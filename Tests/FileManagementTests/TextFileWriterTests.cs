using System;
using System.IO;
using WebApp.Models.FileManagement;
using Xunit;

namespace Tests.FileManagementTests
{
    public class TextFileWriterTests : IDisposable
    {
        private TextFileWriter textFileWriter;
        private string dirPath = "wwwroot/";
        private string filePath = "test.txt";

        public TextFileWriterTests()
        {
            textFileWriter = new TextFileWriter();

            Directory.CreateDirectory(dirPath);
        }

        public void Dispose()
        {
            Directory.Delete(dirPath, true);
        }

        [Fact]
        public void CreateFileWithGivenNameIfNotExists()
        {
            textFileWriter.SaveFile(dirPath + "tt.txt", "TDDS");

            Assert.True(File.Exists(dirPath + "tt.txt"));
            File.Delete(dirPath + "tt.txt");
        }

        [Fact]
        public void SaveFileDataToNewlyCreatedFile()
        {
            textFileWriter.SaveFile(dirPath + "tt.txt", "TDDS");
            string content;
            using (var sr = new StreamReader(dirPath + "tt.txt"))
            {
                content = sr.ReadToEnd();
            }

            Assert.Equal("TDDS", content);
        }

        [Fact]
        public void RewriteExistingFile()
        {
            using (var sw = new StreamWriter(dirPath + "tt.txt"))
            {
                sw.Write("someting else");
            }

            textFileWriter.SaveFile(dirPath + "tt.txt", "TDDS");

            string content;
            using (var sr = new StreamReader(dirPath + "tt.txt"))
            {
                content = sr.ReadToEnd();
            }

            Assert.Equal("TDDS", content);
        }
    }
}
